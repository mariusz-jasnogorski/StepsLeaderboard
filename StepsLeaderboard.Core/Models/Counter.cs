using System.ComponentModel.DataAnnotations;

namespace StepsLeaderboard.Core.Models
{
    public class Counter
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TeamId { get; set; }

        [Required(ErrorMessage = "Owner name is required")]
        [StringLength(50)]
        public string OwnerName { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Steps must be a valid non-negative integer")]
        public int Steps { get; set; }
    }
}
