using System.ComponentModel.DataAnnotations;

namespace StepsLeaderboard.Core.Models
{
    public class Team
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Team name is required")]
        [StringLength(100, ErrorMessage = "Team name can't exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        public List<Guid> CounterIds { get; set; } = new();
    }
}
