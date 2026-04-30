using AttivaMente.Core.Models;

namespace AttivaMente.Data
{
    public class UtenteRepository
    {
        private readonly Database _db;

        public UtenteRepository(string connStr)
        {
            _db = new Database(connStr);
        }

        public List<Utente> GetAll()
        {
            var utenti = new List<Utente>();
            using var reader = _db.ExecuteReader("SELECT * FROM Utenti");
            while (reader.Read())
            {
                utenti.Add(new Utente
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Cognome = reader.GetString(2),
                    Email = reader.GetString(3),
                    PasswordHash = reader.GetString(4),
                    RuoloId = reader.GetInt32(5)
                });

            }
            return utenti;
        }

        public Utente? GetById(int id)
        {
            using var reader = _db.ExecuteReader($"SELECT * FROM Utenti WHERE Id = {id}");
            if (reader.Read())
            {
                return new Utente
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Cognome = reader.GetString(2),
                    Email = reader.GetString(3),
                    PasswordHash = reader.GetString(4),
                    RuoloId = reader.GetInt32(5)
                };
            }
            return null;
        }

        public int Add(Utente utente)
        {
            return _db.ExecuteNonQuery($"INSERT INTO Utenti (Nome, Cognome, Email, PasswordHash, RuoloId) " +
                                $"VALUES ('{utente.Nome}', '{utente.Cognome}', '{utente.Email}', '{utente.PasswordHash}', {utente.RuoloId})");
        }

        public int Delete(int id)
        {
            return _db.ExecuteNonQuery($"DELETE FROM Utenti WHERE Id = {id}");
        }
    }
}
