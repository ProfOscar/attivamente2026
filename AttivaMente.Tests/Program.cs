using AttivaMente.Core.Models;
using AttivaMente.Data;

string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Dati\\AttivaMenteDB.mdf;Integrated Security=True;Connect Timeout=30";

UtenteRepository utenteRepository = new UtenteRepository(connStr);
RuoloRepository ruoloRepository = new RuoloRepository(connStr);

char scelta;
do
{
    Console.Clear();

    Console.WriteLine("--- AttivaMente - Funzionalità di Test ---");
    Console.WriteLine("\n1) Lista Ruoli");
    Console.WriteLine("2) Lista Utenti");
    Console.WriteLine("\nq) ESCI");

    Console.Write("\nScegli la funzione: ");
    scelta = Console.ReadKey().KeyChar;

    switch (scelta)
    {
        case '1':
            ListaRuoli();
            break;
        case '2':
            ListaUtenti();
            break;
        case 'q':
        case 'Q':
            break;
        default:
            Console.WriteLine("\n\nScelta non valida, riprova...");
            Console.ReadKey();
            break;
    }
} while (scelta.ToString().ToLower() != "q");

void ListaRuoli()
{
    Console.Clear();

    List<Ruolo> ruoli = ruoloRepository.GetAll();

    Console.WriteLine("Lista Ruoli:");
    foreach (var ruolo in ruoli)
    {
        Console.WriteLine(ruolo);
    }

    Console.ReadKey();
}

void ListaUtenti()
{
    Console.Clear();

    List<Utente> utenti = utenteRepository.GetAll();

    Console.WriteLine("Lista Utenti:");
    foreach (var utente in utenti)
    {
        Console.WriteLine(utente);
    }

    Console.ReadKey();
}

