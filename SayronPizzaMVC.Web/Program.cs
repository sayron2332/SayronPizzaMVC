using SayronPizzaMVC.Core;
using Walter.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connStr = builder.Configuration.GetConnectionString("DefaultConnection");

// Database context
builder.Services.AddDbContext(connStr);

// Add Core Services
builder.Services.AddCoreServices();
builder.Services.AddMapping();

// Add Infrastructure Services
builder.Services.AddInfrastructureService();

builder.Services.AddRepositories();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
   
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//await UsersAndRolesInitializer.SeedUsersAndRoles(app);
app.Run();
