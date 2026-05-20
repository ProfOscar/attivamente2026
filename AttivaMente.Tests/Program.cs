using AttivaMente.Core.Models;
using AttivaMente.Core.Security;
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
    Console.WriteLine("5) Cancellazione utente");
    Console.WriteLine("----------");
    Console.WriteLine("6) Lista Ruoli");
    Console.WriteLine("7) Ricerca ruolo per ID");
    Console.WriteLine("8) Nuovo ruolo");
    Console.WriteLine("9) Modifica ruolo");
    Console.WriteLine("0) Cancellazione ruolo");
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
        case '3':
            NuovoOrModificaUtente(false);
            break;
        case '4':
            NuovoOrModificaUtente(true);
            break;
        case '5':
            CancellaUtente();
            break;
        case '6':
            ListaRuoli();
            break;
        case '7':
            RicercaRuolo();
            break;
        case '8':
            NuovoOrModificaRuolo(false);
            break;
        case '9':
            NuovoOrModificaRuolo(true);
            break;
        case '0':
            CancellaRuolo();
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

void CancellaRuolo()
{
    Console.Clear();
    Console.WriteLine("CANCELLA RUOLO\n");

    int id;
    do
    {
        Console.Write("Inserisci l'ID del ruolo da cancellare: ");
    }
    while (!int.TryParse(Console.ReadLine(), out id));

    Ruolo? r = ruoloRepository.GetById(id);
    if (r == null)
    {
        Console.WriteLine("Ruolo non trovato!");
        Console.ReadKey();
        return;
    }

    Console.WriteLine($"Ruolo da cancellare: {r}");
    Console.Write("Confermi la cancellazione? (s/n): ");
    string? st = Console.ReadKey().KeyChar.ToString();

    if (string.IsNullOrEmpty(st) || st.ToLower() != "s")
        Console.WriteLine("\nCancellazione abortita");
    else
    {
        if (ruoloRepository.Delete(id))
            Console.WriteLine("\nRuolo cancellato correttamente");
        else
            Console.WriteLine("\nErrore nella cancellazione del ruolo");
    }

    Console.ReadKey();
}

void CancellaUtente()
{
    Console.Clear();
    Console.WriteLine("CANCELLA UTENTE\n");

    int id;
    do
    {
        Console.Write("Inserisci l'ID dell'utente da cancellare: ");
    }
    while (!int.TryParse(Console.ReadLine(), out id));

    Utente? u = utenteRepository.GetById(id);
    if (u == null)
    {
        Console.WriteLine("Utente non trovato!");
        Console.ReadKey();
        return;
    }

    Console.WriteLine($"Utente da cancellare: {u}");
    Console.Write("Confermi la cancellazione? (s/n): ");
    string? st = Console.ReadKey().KeyChar.ToString();

    if (string.IsNullOrEmpty(st) || st.ToLower() != "s")
        Console.WriteLine("\nCancellazione abortita");
    else
    {
        if (utenteRepository.Delete(id))
            Console.WriteLine("\nUtente cancellato correttamente");
        else
            Console.WriteLine("\nErrore nella cancellazione dell'utente");
    }

    Console.ReadKey();
}

void NuovoOrModificaRuolo(bool isModifica)
{
    Console.Clear();

    Console.WriteLine(isModifica ? "MODIFICA RUOLO\n" : "NUOVO RUOLO\n");

    int id = 0;
    if (isModifica)
    {
        do
        {
            Console.Write("Inserisci l'ID del ruolo da modificare: ");
        }
        while (!int.TryParse(Console.ReadLine(), out id));

        Ruolo? r = ruoloRepository.GetById(id);
        if (r == null)
        {
            Console.WriteLine("Ruolo non trovato!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Dati da modificare: {r}");
    }

    Console.Write("Nome: "); string? nome = Console.ReadLine();

    if (string.IsNullOrEmpty(nome))
    {
        Console.WriteLine("Dati non corretti, riprova...");
        Console.ReadKey();
        return;
    }

    var ruolo = new Ruolo { Nome = nome, };

    bool retVal = isModifica ?
        ruoloRepository.Update(ruolo, id) :
        ruoloRepository.Add(ruolo);

    if (retVal)
        Console.WriteLine("Ruolo aggiunto o modificato correttamente");
    else
        Console.WriteLine("Errore nell'aggiunta o modifica del ruolo");

    Console.ReadKey();
}

void NuovoOrModificaUtente(bool isModifica)
{
    Console.Clear();

    Console.WriteLine(isModifica ? "MODIFICA UTENTE\n" : "NUOVO UTENTE\n");

    int id = 0;
    if (isModifica)
    {
        do
        {
            Console.Write("Inserisci l'ID dell'utente da modificare: ");
        }
        while (!int.TryParse(Console.ReadLine(), out id));

        Utente? u = utenteRepository.GetById(id);
        if (u == null)
        {
            Console.WriteLine("Utente non trovato!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Dati da modificare: {u}");
    }

    Console.Write("Nome: "); string? nome = Console.ReadLine();
    Console.Write("Cognome: "); string? cognome = Console.ReadLine();
    Console.Write("Email: "); string? email = Console.ReadLine();
    Console.Write("Password: "); string? password = Console.ReadLine();
    int ruoloId;
    do
    {
        Console.Write("ID ruolo: ");
    }
    while (!int.TryParse(Console.ReadLine(), out ruoloId));

    if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cognome) || string.IsNullOrEmpty(email)
        || string.IsNullOrEmpty(password) || ruoloId < 1)
    {
        Console.WriteLine("Dati non corretti, riprova...");
        Console.ReadKey();
        return;
    }

    var utente = new Utente
    {
        Nome = nome,
        Cognome = cognome,
        Email = email,
        PasswordHash = PasswordHelper.HashPassword(password),
        RuoloId = ruoloId,
    };

    bool retVal = isModifica ? 
        utenteRepository.Update(utente, id) : 
        utenteRepository.Add(utente);

    if (retVal)
        Console.WriteLine("Utente aggiunto o modificato correttamente");
    else
        Console.WriteLine("Errore nell'aggiunta o modifica dell'utente");

    Console.ReadKey();
}

void RicercaRuolo()
{
    Console.Clear();

    int id;
    do
    {
        Console.Write("Inserisci l'ID del ruolo da cercare: ");
    }
    while (!int.TryParse(Console.ReadLine(), out id));

    Ruolo? ruolo = ruoloRepository.GetById(id);
    if (ruolo == null)
        Console.WriteLine("Ruolo non trovato");
    else
        Console.WriteLine(ruolo);

    Console.ReadKey();
}

void RicercaUtente()
{
    Console.Clear();

    int id;
    do
    {
        Console.Write("Inserisci l'ID dell'utente da cercare: ");
    }
    while (!int.TryParse(Console.ReadLine(), out id));

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

