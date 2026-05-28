using AttivaMente.Core.Models;
using AttivaMente.Core.Security;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var utente = new Utente()
{
    Nome = "Mario",
    Cognome = "Rossi",
    Email = "mario@example.com",
    PasswordHash = PasswordHelper.HashPassword("MiaPwd50@"),
    RuoloId = 1
};

app.MapGet("/", () => "Elenco Utenti:\n" + utente);

app.MapGet("admin/", () => "Admin page (login required)");

app.Run();
