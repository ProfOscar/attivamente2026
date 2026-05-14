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

        public bool Add(Utente u)
        {
            string sql = @$"INSERT INTO Utenti (Nome, Cognome, Email, PasswordHash, RuoloId) 
                    VALUES ('{u.Nome}', '{u.Cognome}', '{u.Email}', '{u.PasswordHash}', {u.RuoloId})";
            return CallExecuteNonQuery(sql);
        }

        public bool Update(Utente u, int id)
        {
            string sql = @$"UPDATE Utenti SET 
                    Nome = '{u.Nome}', Cognome = '{u.Cognome}', Email = '{u.Email}',  
                    PasswordHash = '{u.PasswordHash}', RuoloId = {u.RuoloId} 
                    WHERE Id = {id}";
            return CallExecuteNonQuery(sql);
        }

        private bool CallExecuteNonQuery(string sql)
        {
            try
            {
                int retVal = _db.ExecuteNonQuery(sql);
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
