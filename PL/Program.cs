using BLL.Services;
using BLL.UnitOfWork;
using DAL;
using Lab3_3.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TheaterDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PL"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISpectacleService, SpectacleService>();
builder.Services.AddScoped<ITicketService, TicketService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<TheaterDbContext>();
await context.Database.MigrateAsync();

var unitOfWork = new UnitOfWork(context);

await Demo.RunAsync(new SpectacleService(unitOfWork), new TicketService(unitOfWork));
