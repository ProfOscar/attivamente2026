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
            string query = $"SELECT Id, Nome FROM Ruoli WHERE Id={id}";

            using var reader = _db.ExecuteReader(query);
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
            string sql = @$"INSERT INTO Ruoli (Nome) VALUES ('{r.Nome}')";
            return CallExecuteNonQuery(sql);
        }

        public bool Update(Ruolo r, int id)
        {
            string sql = $"UPDATE Ruoli SET Nome = '{r.Nome}' WHERE Id = {id}";
            return CallExecuteNonQuery(sql);
        }

        public bool Delete(int id)
        {
            string sql = $"DELETE FROM Ruoli WHERE Id = {id}";
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
