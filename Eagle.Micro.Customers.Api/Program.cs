// var app = WebApplication.Create();
using Bogus;
using Customers.Domain;
using Customers.Domain.Models;
using Customers.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

builder.Services.AddScoped<ICustomerRepository, DbCustomerRepository>();

// dotnet add package Microsoft.EntityFrameworkCore.Sqlite
builder.Services.AddDbContext<CustomersContext>(options => options.UseSqlite("Filename=customers.db"));

// builder.Services.AddDbContextPool<CustomersContext>(options => options.UseSqlite("Filename=customers.db"));

var app = builder.Build();

app.MapGet("api/customers/ping", () => Results.Ok());

// TODO: Status code 418 :)

app.MapGet("api/customers/hello", (HttpRequest req, HttpResponse res) => res.WriteAsync("Hello"));

app.MapGet("api/customers/hello2", (HttpContext context) => context.Response.WriteAsync("Hello"));

// GET api/customers
app.MapGet("api/customers", (ICustomerRepository repository, [FromHeader(Name = "x-secret-key")] string secretKey) => repository.GetAllAsync());

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

app.Run();
