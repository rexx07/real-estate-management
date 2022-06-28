using Core.Data;
using Microsoft.EntityFrameworkCore;
using RED.Services.ServiceBase.Interfaces;
using RED.Services.ServiceBase.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RedDbConnection");

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program)); 
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RedDbContext>(options => options.UseNpgsql(connectionString!));

builder.Services.AddScoped<IServiceWrapper, ServiceWrapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<RedDbContext>();
    context.Database.EnsureCreated();
    SeedData.Initialize(context);
}*/

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    "default",
    "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();