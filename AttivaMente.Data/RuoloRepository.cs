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
    }
}
