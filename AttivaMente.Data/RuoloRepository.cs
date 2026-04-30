using AttivaMente.Core.Models;

namespace AttivaMente.Data
{
    public class RuoloRepository
    {
        private readonly Database _db;

        public RuoloRepository(string connStr)
        {
            _db = new Database(connStr);
        }

        public List<Ruolo> GetAll()
        {
            var ruoli = new List<Ruolo>();
            using var reader = _db.ExecuteReader("SELECT Id, Nome FROM Ruoli");
            while (reader.Read())
            {
                ruoli.Add(new Ruolo
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1)
                });
            }
            return ruoli;
        }
    }
}
