using AttivaMente.Core.Models;
using AttivaMente.Core.Security;
using AttivaMente.Data;

string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Dati\\AttivaMenteDB_4E.mdf;Integrated Security=True;Connect Timeout=30";
RuoloRepository ruoloRepository = new RuoloRepository(connStr);
UtenteRepository utenteRepository = new UtenteRepository(connStr);


char scelta;
do
{
    Console.Clear();

    Console.WriteLine("--- AttivaMente - Funzionalità di Test ---\n");
    Console.WriteLine("1) Lista Ruoli");
    Console.WriteLine("2) Lista Utenti");
    Console.WriteLine("3) Cerca Ruolo per ID");
    Console.WriteLine("4) Cerca Utente per ID");
    Console.WriteLine("5) Aggiungi Ruolo");
    Console.WriteLine("6) Aggiungi Utente");
    Console.WriteLine("7) Modifica Ruolo");
    Console.WriteLine("8) Modifica Utente");
    Console.WriteLine("9) Elimina Ruolo");
    Console.WriteLine("A) Elimina Utente");
    Console.WriteLine("\nq) ESCI");

    Console.Write("\nScegli la funzione: ");
    scelta = Console.ReadKey().KeyChar;

    switch (scelta)
    {
        case '1':
            Console.Clear();
            ListaRuoli();
            break;
        case '2':
            ListaUtenti();
            break;
        case '3':
            CercaRuoloPerID();
            break;
        case '4':
            CercaUtentePerID();
            break;
        case '5':
            AggiungiRuolo();
            break;
        case '6':
            AggiungiUtente();
            break;
        case '9':
            EliminaRuolo();
            break;
        case 'a':
        case 'A':
            EliminaUtente();
            break;
        case 'q':
        case 'Q':
            break;
        default:
            Console.WriteLine("\n\nScelta non valida, riprova...");
            break;
    }
    if (scelta.ToString().ToLower() != "q") Console.ReadKey();
} while (scelta.ToString().ToLower() != "q");

void ListaRuoli()
{
    Console.Clear();
    List<Ruolo> ruoli = ruoloRepository.GetAll();
    Console.WriteLine("Lista Ruoli:");
    foreach (var ruolo in ruoli)
        Console.WriteLine(ruolo);
}

void ListaUtenti()
{
    Console.Clear();
    List<Utente> utenti = utenteRepository.GetAll();
    Console.WriteLine("Lista Utenti:");
    foreach (var utente in utenti)
        Console.WriteLine(utente);
}

void CercaRuoloPerID()
{
    try
    {
        Console.Write("\n\nInserisci l'ID da cercare: ");
        int id = int.Parse(Console.ReadLine()!);
        Ruolo? ruolo = ruoloRepository.GetById(id);
        if (ruolo == null)
            Console.WriteLine("Ruolo non trovato!");
        else
            Console.WriteLine(ruolo);
    }
    catch
    {
        Console.WriteLine("ERRORE: controlla i dati!");
    }
}

void CercaUtentePerID()
{
    try
    {
        Console.Write("\n\nInserisci l'ID da cercare: ");
        int id = int.Parse(Console.ReadLine()!);
        Utente? utente = utenteRepository.GetById(id);
        if (utente == null)
            Console.WriteLine("Utente non trovato!");
        else
            Console.WriteLine(utente);
    }
    catch
    {
        Console.WriteLine("ERRORE: controlla i dati!");
    }
}

void AggiungiRuolo()
{
    try
    {
        Console.Write("\n\nInserisci il nuovo ruolo: ");
        string nome = Console.ReadLine()!;
        if (ruoloRepository.Add(nome) > 0)
            Console.WriteLine($"Ruolo {nome} aggiunto correttamente");
        else
            Console.WriteLine("Ruolo non aggiunto!");
    }
    catch
    {
        Console.WriteLine("\nERRORE: controlla i dati!");
    }
}

void AggiungiUtente()
{
    try
    {
        Console.Write("\n\nInserisci il nome: ");
        string nome = Console.ReadLine()!;
        Console.Write("Inserisci il cognome: ");
        string cognome = Console.ReadLine()!;
        Console.Write("Inserisci l'email: ");
        string email = Console.ReadLine()!;
        Console.Write("Inserisci la password: ");
        string pwdClear = Console.ReadLine()!;
        string pwdCrypted = PasswordHelper.HashPassword(pwdClear);
        Console.Write("Inserisci il codice del ruolo: ");
        int ruoloId = int.Parse(Console.ReadLine()!);

        Utente nuovoUtente = new Utente()
        {
            Nome = nome,
            Cognome = cognome,
            Email = email,
            PasswordHash = pwdCrypted,
            RuoloId = ruoloId
        };


        if (utenteRepository.Add(nuovoUtente) > 0)
            Console.WriteLine($"Utente aggiunto correttamente");
        else
            Console.WriteLine("Utente non aggiunto!");
    }
    catch
    {
        Console.WriteLine("\nERRORE: controlla i dati!");
    }
}

void EliminaRuolo() {
    try
    {
        Console.Write("\n\nInserisci l'ID del ruolo da eliminare: ");
        int id = int.Parse(Console.ReadLine()!);
        if (ruoloRepository.Delete(id) > 0)
            Console.WriteLine($"Ruolo {id} eliminato!");
        else
            Console.WriteLine("Ruolo non eliminato!");
    }
    catch
    {
        Console.WriteLine("ERRORE: controlla i dati!");
    }
}

void EliminaUtente()
{
    try
    {
        Console.Write("\n\nInserisci l'ID dell'utente da eliminare: ");
        int id = int.Parse(Console.ReadLine()!);
        if (utenteRepository.Delete(id) > 0)
            Console.WriteLine($"Utente {id} eliminato!");
        else
            Console.WriteLine("Utente non eliminato!");
    }
    catch
    {
        Console.WriteLine("ERRORE: controlla i dati!");
    }
}
