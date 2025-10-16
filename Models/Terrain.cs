using System.ComponentModel.DataAnnotations;

namespace TP1.Models
{
    public class Terrain
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty; // "5-a-side", "7-a-side", "11-a-side"

        [Required]
        [StringLength(200)]
        public string Localisation { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string? Description { get; set; }

        // Navigation properties
        public ICollection<Creneau> Creneaux { get; set; } = new List<Creneau>();
    }
}


