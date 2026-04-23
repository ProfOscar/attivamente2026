namespace AttivaMente.Core.Models
{
    public class Ruolo
    {
        public int Id { get; set; }
        public string Nome { get; set; } // Admin, Volontario, Coordinatore, Segreteria 

        public override string ToString()
        {
            return $"{Id}: {Nome}";
        }
    }
}
