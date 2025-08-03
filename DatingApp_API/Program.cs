using DatingApp_API.Data;
using DatingApp_API.Extensions;
using DatingApp_API.Middleware;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);


//Below line will work just like above line, but above line is a standard practice to use services
//builder.Services.AddScoped<TokenService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;
try
{
	var context=services.GetRequiredService<DataContext>();
	await context.Database.MigrateAsync();
	await Seed.SeedUsers(context);
}
catch (Exception ex)
{
	var logger=services.GetRequiredService<ILogger<Program>>();
	logger.LogError(ex, "An error occured during migration");
}

app.Run();
