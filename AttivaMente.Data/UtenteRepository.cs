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
            string query = $"SELECT Id, Nome, Cognome, Email, PasswordHash, RuoloId FROM Utenti WHERE Id = @p1";

            using var reader = _db.ExecuteReader(query, id);
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

        public Utente? GetByEmail(string email)
        {
            string query = $"SELECT Id, Nome, Cognome, Email, PasswordHash, RuoloId FROM Utenti WHERE Email = @p1";

            using var reader = _db.ExecuteReader(query, email);
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

        public bool Add(Utente u)
        {
            string sql = @$"INSERT INTO Utenti (Nome, Cognome, Email, PasswordHash, RuoloId) 
                    VALUES (@p1, @p2, @p3, @p4, @p5)";
            return CallExecuteNonQuery(sql, u.Nome, u.Cognome, u.Email, u.PasswordHash, u.RuoloId);
        }

        public bool Update(Utente u, int id)
        {
            string sql = @$"UPDATE Utenti SET 
                    Nome = @p1, Cognome = @p2, Email = @p3,  
                    PasswordHash = @p4, RuoloId = @p5 
                    WHERE Id = @p6";
            return CallExecuteNonQuery(sql, u.Nome, u.Cognome, u.Email, u.PasswordHash, u.RuoloId, id);
        }

        public bool Delete(int id)
        {
            string sql = $"DELETE FROM Utenti WHERE Id = @p1";
            return CallExecuteNonQuery(sql, id);
        }

        private bool CallExecuteNonQuery(string sql, params object[] parameters)
        {
            try
            {
                int retVal = _db.ExecuteNonQuery(sql, parameters);
                return retVal == 1; // ritorno true solo se è stato inserito 1 record
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return false;
            }
        }
    }
}
