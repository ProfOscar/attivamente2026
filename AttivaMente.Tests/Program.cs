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
            Console.Clear();
            Console.WriteLine("Hai scelto 2");
            Console.ReadKey();
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
