using AttivaMente.Core.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AttivaMente.Data
{
    public class UtenteRepository
    {
        private readonly Database _db;

        public UtenteRepository(string connectionString)
        {
            _db = new Database(connectionString);
        }

        public List<Utente> GetAll()
        {
            var utenti = new List<Utente>();
            string query = @"SELECT u.Id, u.Nome, Cognome, Email, PasswordHash, RuoloId, r.Nome AS RuoloNome
                             FROM Utenti u INNER JOIN Ruoli r ON u.RuoloId = r.Id";

            using var reader = _db.ExecuteReader(query);
            while (reader.Read())
            {
                var utente = MapUtente(reader);
                utenti.Add(utente);
            }

            return utenti;
        }

        public Utente? GetById(int id)
        {
            string query = @"SELECT u.Id, u.Nome, Cognome, Email, PasswordHash, RuoloId, r.Nome AS RuoloNome
                             FROM Utenti u INNER JOIN Ruoli r ON u.RuoloId = r.Id
                             WHERE u.Id = @p1";

            using var reader = _db.ExecuteReader(query, id);
            if (reader.Read())
            {
                var utente = MapUtente(reader);
                return utente;
            }

            return null;
        }

        public Utente? GetByEmail(string email)
        {
            string query = @"SELECT u.Id, u.Nome, Cognome, Email, PasswordHash, RuoloId, r.Nome AS RuoloNome
                             FROM Utenti u INNER JOIN Ruoli r ON u.RuoloId = r.Id
                             WHERE Email = @p1";

            using var reader = _db.ExecuteReader(query, email);
            if (reader.Read())
            {
                var utente = MapUtente(reader);
                return utente;
            }

            return null;
        }

        public List<Utente> Search(string searchTerm)
        {
            var utenti = new List<Utente>();
            var pattern = $"%{searchTerm}%";

            string query = @"SELECT u.Id, u.Nome, u.Cognome, u.Email, u.PasswordHash, u.RuoloId, r.Nome AS RuoloNome
                             FROM Utenti u 
                             INNER JOIN Ruoli r ON u.RuoloId = r.Id
                             WHERE u.Nome LIKE @p1 OR u.Cognome LIKE @p1 OR u.Email LIKE @p1";

            using var reader = _db.ExecuteReader(query, pattern);
            while (reader.Read())
            {
                utenti.Add(MapUtente(reader));
            }

            return utenti;
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

        private static Utente MapUtente(IDataRecord record)
        {
            return new Utente
            {
                Id = record.GetInt32(0),
                Nome = record.GetString(1),
                Cognome = record.GetString(2),
                Email = record.GetString(3),
                PasswordHash = record.IsDBNull(4) ? "" : record.GetString(4),
                RuoloId = record.GetInt32(5),
                Ruolo = new Ruolo
                {
                    Id = record.GetInt32(5),
                    Nome = record.GetString(6)
                }
            };
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
                throw;
            }
        }
    }
}
