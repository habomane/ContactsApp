using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ContactsAPI.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ContactDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConnection")));
builder.Services.AddScoped<IContactRepository, ContactRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
