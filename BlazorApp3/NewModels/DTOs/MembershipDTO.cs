using System.ComponentModel.DataAnnotations;

namespace BlazorApp3.NewModels.DTOs
{
    public class MembershipDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(25)]
        public string? Shortname { get; set; }

        public bool Wheelchair { get; set; }
    }
}
