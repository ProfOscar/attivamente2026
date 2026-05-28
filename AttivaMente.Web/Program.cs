// Il builder permette di costruire la web application
var builder = WebApplication.CreateBuilder(args);

// Aggiungo i servizi per poter usare MVC
builder.Services.AddControllersWithViews();

// Costruisco la web application
var app = builder.Build();

// Abilito i file statici (CSS, JS, immagini, ...)
app.UseStaticFiles();

// Configuro routing per utilizzare i Controller e le View
app.UseRouting();

// Imposto la route predefinita per i Controller della web application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Lancio la web application
app.Run();
