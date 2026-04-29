using AttivaMente.Core.Models;
using AttivaMente.Data;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;

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

    CaricaUtentiDaDB(utenti);

    Console.WriteLine("Lista Utenti:");
    foreach (var utente in utenti)
    {
        Console.WriteLine(utente);
    }

    Console.ReadKey();
}

void CaricaUtentiDaDB(List<Utente> utenti)
{
    string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Dati\\AttivaMenteDB.mdf;Integrated Security=True;Connect Timeout=30";
    Database db = new Database(connStr);

    using (SqlDataReader reader = db.ExecuteReader("SELECT * FROM Utenti"))
    {
        while (reader.Read())
        {
            string nome = reader[1].ToString();
            string cognome = reader[2].ToString();
            string email = reader[3].ToString();
            string pwHash = reader[4].ToString();
            Utente utente = new Utente()
            {
                Nome = nome,
                Cognome = cognome,
                Email = email,
                PasswordHash = pwHash
            };
            utenti.Add(utente);
        }
    }
}