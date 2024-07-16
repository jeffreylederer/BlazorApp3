using BlazorApp3.Model;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp3.NewModels.DTOs
{
    public class LeagueDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string LeagueName { get; set; } = null!;

        public bool Active { get; set; }

        [Required]
        [RegularExpression("[1|2|3]", ErrorMessage = "Team size must be 1, 2 or 3")]
        public int TeamSize { get; set; }

        public bool TiesAllowed { get; set; }

        public bool PointsCount { get; set; }

        [Required]
        [Range(0, 3, MinimumIsExclusive = true, MaximumIsExclusive = true)]
        public short WinPoints { get; set; }

        [Required]
        [Range(0, 3, MinimumIsExclusive = true, MaximumIsExclusive = true)]
        public short TiePoints { get; set; }

        [Required]
        [Range(0, 3, MinimumIsExclusive = true, MaximumIsExclusive = true)]
        public short ByePoints { get; set; }

        [Required]
        [Range(0, 10, MinimumIsExclusive = true, MaximumIsExclusive = true)]
        public int StartWeek { get; set; }

        public bool PointsLimit { get; set; }

        [Required]
        [RegularExpression("[1|2]", ErrorMessage = "Divisions must be 1 or 2")]
        public short Divisions { get; set; }

        public bool PlayOffs { get; set; }
    }

       
}
