using BLL.Services;
using BLL.UnitOfWork;
using DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TheaterDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PL"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISpectacleService, SpectacleService>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddControllersWithViews().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TheaterDbContext>();
    await context.Database.MigrateAsync();
}

await app.RunAsync();
