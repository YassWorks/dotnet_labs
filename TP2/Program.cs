using Microsoft.EntityFrameworkCore;
using TP2.Interceptors;
using TP2.Models;
using TP2.Repositories;
using TP2.Services.ServiceContracts;
using TP2.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Register AuditInterceptor as a scoped service
builder.Services.AddScoped<AuditInterceptor>();

builder.Services.AddDbContext<ApplicationDbContext>(
    (serviceProvider, options) => options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .AddInterceptors(serviceProvider.GetRequiredService<AuditInterceptor>()));

// Register Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register Specific Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

// Register services
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IMembershipTypeService, MembershipTypeService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Database initialization removed: seeding disabled

app.Run();
