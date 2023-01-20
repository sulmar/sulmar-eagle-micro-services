// var app = WebApplication.Create();
using Bogus;
using Customers.Api;
using Customers.Domain;
using Customers.Domain.Models;
using Customers.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder();

builder.Services.AddHealthChecks()
    .AddCheck<MachineHealthCheck>("Machine")
    .AddCheck("Ping", () => HealthCheckResult.Healthy())
    .AddCheck("Random", () =>
    {
        if (DateTime.Now.Minute % 2 == 0)        
            return HealthCheckResult.Healthy();
        else
            return HealthCheckResult.Unhealthy();
    });

// dotnet add package AspNetCore.HealthChecks.UI
// dotnet add package AspNetCore.HealthChecks.UI.InMemory.Storage
//builder.Services.AddHealthChecksUI(options =>
//{
//    options.SetEvaluationTimeInSeconds(10);
//    options.AddHealthCheckEndpoint("Customers Api", "/health");
//}).AddSqliteStorage("Filename=customers.db");

builder.Services.AddCors(options =>
{   
    options.AddPolicy("AllowFrontent", policy =>
    {        
        policy.WithOrigins("http://localhost:5173");
        policy.WithMethods("GET", "POST");
        policy.AllowAnyHeader();
        // policy.WithHeaders("x-secret-key");
    });
});

builder.Services.AddScoped<ICustomerRepository, DbCustomerRepository>();

builder.Services.AddAuthorization();

// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    string secretKey = "your-256-bit-secret";

    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = true,
        ValidIssuer = "http://eagle.pl",
        ValidateAudience = true,
        ValidAudience = "http://sulmar.pl",
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["your-cookie"];
            return Task.CompletedTask;
        }
    };
});

// dotnet add package Microsoft.EntityFrameworkCore.Sqlite
builder.Services.AddDbContext<CustomersContext>(options => options.UseSqlite("Filename=customers.db"));

// builder.Services.AddDbContextPool<CustomersContext>(options => options.UseSqlite("Filename=customers.db"));

var app = builder.Build();


app.UseCors("AllowFrontent");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("api/customers/ping", () => Results.Ok());

// TODO: Status code 418 :)

app.MapGet("api/customers/hello", (HttpRequest req, HttpResponse res) => res.WriteAsync("Hello"));

app.MapGet("api/customers/hello2", (HttpContext context) => context.Response.WriteAsync("Hello"));

app.MapGet("api/customers/hello/{name}", (HttpContext context, string name, ILogger<Program> logger) =>
{
    context.Response.WriteAsync($"Hello {name}");

    // z³a praktyka
    // logger.LogInformation($"Hello {name}");

    // dobra praktyka
    logger.LogInformation("Hello {name}", name);
});

// GET api/customers
app.MapGet("customers", (ICustomerRepository repository, [FromHeader(Name = "x-secret-key")] string secretKey) => repository.GetAllAsync())
    .RequireAuthorization();

// app.MapGet("api/customers/{id}", (int id, ICustomerRepository repository) => repository.GetByIdAsync(id));

//app.MapGet("api/customers/{id}", async (int id, ICustomerRepository repository) =>
//{
//    var customer = await repository.GetByIdAsync(id);

//    if (customer == null)
//        return Results.NotFound();

//    return Results.Ok(customer);
//});

//app.MapGet("api/customers/{id}", async (int id, ICustomerRepository repository) => 
//    await repository.GetByIdAsync(id) is Customer customer 
//    ? Results.Ok(customer) : Results.NotFound() );

app.MapGet("api/customers/{id}", async (int id, ICustomerRepository repository) => await repository.GetByIdAsync(id) switch
    {
        
        Customer customer => Results.Ok(customer),
        null => Results.NotFound()
    });


// api/customers?city=Wa³cz&country=Poland
// api/customers?lat={lat}&lng={lng}

app.MapPost("api/customers", (Customer customer, ICustomerRepository repository) => Results.Ok());


using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<CustomersContext>();
//
// context.Database.EnsureCreated(); 
context.Database.Migrate();

if (!context.Customers.Any())
{
    var addresses = new Faker<Address>()
        .RuleFor(p => p.City, f => f.Address.City())
        .RuleFor(p => p.Country, f => f.Address.Country())
        .Generate(100);

    var customers = new Faker<Customer>()
        .RuleFor(p => p.Name, f => f.Company.CompanyName())
        .RuleFor(p => p.WorkAddress, f => f.PickRandom(addresses))
        .Generate(100);

    context.Customers.AddRange(customers);
    context.SaveChanges();
}


// dotnet add package AspNetCore.HealthChecks.UI.Client
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    // ResponseWriter = (context, report) => { repo.}
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

});

// app.MapHealthChecksUI(); // /healthchecks-ui

app.Run();
