using System.ComponentModel.DataAnnotations;

namespace BlazorApp3.NewModels.DTOs
{
    public class ScheduleDTO
    {
        public int Id { get; set; }

        [Required]
        public DateOnly GameDate { get; set; }

        public int Leagueid { get; set; }

        public bool Cancelled { get; set; }

        public bool PlayOffs { get; set; }
    }
}
