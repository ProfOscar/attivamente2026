using AttivaMente.Core.Models;

namespace AttivaMente.Data
{
    public class RuoloRepository
    {
        private readonly Database _db;

        public RuoloRepository(string connectionString)
        {
            _db = new Database(connectionString);
        }

        public List<Ruolo> GetAll()
        {
            var ruoli = new List<Ruolo>();
            string query = "SELECT Id, Nome FROM Ruoli";

            using var reader = _db.ExecuteReader(query);
            while (reader.Read())
            {
                var ruolo = new Ruolo
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                };
                ruoli.Add(ruolo);
            }

            return ruoli;
        }

        public Ruolo? GetById(int id)
        {
            string query = $"SELECT Id, Nome FROM Ruoli WHERE Id = @p1";

            using var reader = _db.ExecuteReader(query, id);
            if (reader.Read())
            {
                var ruolo = new Ruolo
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                };
                return ruolo;
            }

            return null;
        }

        public bool Add(Ruolo r)
        {
            string sql = @$"INSERT INTO Ruoli (Nome) VALUES (@p1)";
            return CallExecuteNonQuery(sql, r.Nome);
        }

        public bool Update(Ruolo r, int id)
        {
            string sql = $"UPDATE Ruoli SET Nome = @p1 WHERE Id = @p2";
            return CallExecuteNonQuery(sql, r.Nome, id);
        }

        public bool Delete(int id)
        {
            string sql = $"DELETE FROM Ruoli WHERE Id = @p1";
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
