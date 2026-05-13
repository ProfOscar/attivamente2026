using AttivaMente.Core.Models;
using AttivaMente.Data;

string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Dati\\AttivaMenteDB.mdf;Integrated Security=True;Connect Timeout=30";

UtenteRepository utenteRepository = new UtenteRepository(connStr);
RuoloRepository ruoloRepository = new RuoloRepository(connStr);

char scelta;
do
{
    Console.Clear();

    Console.WriteLine("--- AttivaMente - Funzionalità di Test ---\n");
    Console.WriteLine("1) Lista Utenti");
    Console.WriteLine("2) Ricerca utente per ID");
    Console.WriteLine("3) Nuovo utente");
    Console.WriteLine("4) Modifica utente");
    Console.WriteLine("5) Cancelazione utente");
    Console.WriteLine("----------");
    Console.WriteLine("6) Lista Ruoli");
    Console.WriteLine("----------");
    Console.WriteLine("q) ESCI");

    Console.Write("\nScegli la funzione: ");
    scelta = Console.ReadKey().KeyChar;

    switch (scelta)
    {
        case '1':
            ListaUtenti();
            break;
        case '2':
            RicercaUtente();
            break;
        case '6':
            ListaRuoli();
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

void RicercaUtente()
{
    Console.Clear();
    Console.Write("Inserisci l'ID dell'utente da cercare: ");
    int id = int.Parse(Console.ReadLine()!);
    Utente? utente = utenteRepository.GetById(id);
    if (utente == null)
        Console.WriteLine("Utente non trovato");
    else
        Console.WriteLine(utente);

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

