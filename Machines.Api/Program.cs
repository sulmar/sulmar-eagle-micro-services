global using FastEndpoints;
global using Machines.Domain;
using Bogus;
using Machines.Infrastucture;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();
builder.Services.AddFastEndpoints();

builder.Services.AddScoped<IMachineRepository, DbMachineRepository>();

// dotnet add package Microsoft.EntityFrameworkCore.Sqlite
builder.Services.AddDbContext<MachineContext>(options => options.UseSqlite("Filename=customers.db"));

// builder.Services.AddDbContextPool<CustomersContext>(options => options.UseSqlite("Filename=customers.db"));

var app = builder.Build();


// dotnet add package Ardalis.ApiEndpoints

app.MapGet("/", () => "Hello World!");

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MachineContext>();

context.Database.EnsureCreated(); 

//context.Database.Migrate();

if (!context.Machines.Any())
{
    var machines = new Faker<Machine>()
        .StrictMode(true)
        .Ignore(p=>p.Id)
        .RuleFor(p => p.Name, f => f.Lorem.Word())   
        .RuleFor(p=>p.SerialNumber, f=>f.Commerce.Ean13())
        .Generate(100);

    context.Machines.AddRange(machines);
    context.SaveChanges();
}

app.UseFastEndpoints();
app.Run();
