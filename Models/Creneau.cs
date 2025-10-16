using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP1.Models
{
    public class Creneau
    {
        public int Id { get; set; }

        [Required]
        public int TerrainId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan HeureDebut { get; set; }

        [Required]
        public TimeSpan HeureFin { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Prix { get; set; }

        [Required]
        public int Capacite { get; set; }

        [Required]
        public int PlacesRestantes { get; set; }

        public bool EstDisponible => PlacesRestantes > 0;

        // Navigation properties
        public Terrain Terrain { get; set; } = null!;
        public ICollection<PanierItem> PanierItems { get; set; } = new List<PanierItem>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}


