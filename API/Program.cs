using API.Data;
using API.Data.SeedData;
using API.Extensions;
using API.Middlewares;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
builder.Services.AddApplicationServices(builder.Configuration); //Adding for keep Pipline clean

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlerMiddleware>(); //ErrorHandler
app.UseMiddleware<LoggingMiddleware>();

app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

//Create DataBase Sqlite and Fill Seed Data if not exist
try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
	throw;
}
app.Run();
