using BLL.Services;
using BLL.UnitOfWork;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Лабораторна робота 5. Варіант 3.", Version = "v1" });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Лабораторна робота 5. Варіант 3."));

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TheaterDbContext>();
    await context.Database.MigrateAsync();
}

await app.RunAsync();
