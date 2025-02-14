using Microsoft.EntityFrameworkCore;
using TaskManager.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddControllersWithViews();

// Configuración de la base de datos MySQL
builder.Services.AddDbContext<TaskManagerContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("TaskManagerConnection"), 
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("TaskManagerConnection"))));

var app = builder.Build();

// Configuración del pipeline de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Configuración de las rutas
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
