using AttivaMente.Core.Models;

namespace AttivaMente.Data
{
    public class UtenteRepository
    {
        private readonly Database _db;

        public UtenteRepository(string connectionString) {
            _db = new Database(connectionString);
        }

        public List<Utente> GetAll()
        {
            var utenti = new List<Utente>();
            string query = "SELECT Id, Nome, Cognome, Email, PasswordHash, RuoloId FROM Utenti";

            using var reader = _db.ExecuteReader(query);
            while (reader.Read())
            {
                var utente = new Utente
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Cognome = reader.GetString(2),
                    Email = reader.GetString(3),
                    PasswordHash = reader.GetString(4),
                    RuoloId = reader.GetInt32(5)
                };
                utenti.Add(utente);
            }

            return utenti;
        }

        public Utente? GetById(int id)
        {
            string query = $"SELECT Id, Nome, Cognome, Email, PasswordHash, RuoloId FROM Utenti WHERE Id={id}";

            using var reader = _db.ExecuteReader(query);
            if (reader.Read())
            {
                var utente = new Utente
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Cognome = reader.GetString(2),
                    Email = reader.GetString(3),
                    PasswordHash = reader.GetString(4),
                    RuoloId = reader.GetInt32(5)
                };
                return utente;
            }

            return null;
        }
    }
}
