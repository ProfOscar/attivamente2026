using System.ComponentModel.DataAnnotations;

namespace AttivaMente.Core.Models
{
    public class Utente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(50)]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio")]
        [StringLength(50)]
        public required string Cognome { get; set; }
        
        public string? Email { get; set; }
        
        public string? PasswordHash { get; set; }
        
        public int RuoloId { get; set; }
        public Ruolo? Ruolo { get; set; }


        public override string ToString()
        {
            return $"{Id} - {Nome} {Cognome} - {Email} - {RuoloId}";
        }
    }
}
