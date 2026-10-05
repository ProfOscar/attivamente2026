using System.ComponentModel.DataAnnotations;

namespace AttivaMente.Core.Models
{
    public class Ruolo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(50)]
        public required string Nome { get; set; } // Admin, Volontario, Coordinatore, Segreteria 

        public override string ToString()
        {
            return $"{Id}: {Nome}";
        }
    }
}
