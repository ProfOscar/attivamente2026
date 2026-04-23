using AttivaMente.Core.Models;

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
            Console.Clear();
            Console.WriteLine("Hai scelto 1");
            Console.ReadKey();
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

void ListaUtenti()
{
    Console.Clear();

    List<Utente> utenti = new List<Utente>();

    var u = new Utente()
    {
        Id = 1,
        Nome = "Giuseppe",
        Cognome = "Garibaldi",
        Email = "g.garibaldi@italia.com",
    };
    utenti.Add(u);

    u = new Utente()
    {
        Id = 2,
        Nome = "Camillo",
        Cognome = "Cavour",
        Email = "camillo@savoia.it"
    };
    utenti.Add(u);

    Console.WriteLine("Lista Utenti:");
    foreach (var utente in utenti)
    {
        Console.WriteLine(utente);
    }

    Console.ReadKey();
}