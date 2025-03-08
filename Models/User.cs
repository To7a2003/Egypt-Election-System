using ElectionSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty; // Example: Admin, Voter, etc.

        // If you are using relationships with Voter
        public Voter Voter { get; set; } = null!;

        // Relationship with Elections supervised by the User
        public ICollection<Election> SupervisedElections { get; set; } = new List<Election>();

        // Relationship with Elections created by the User
        public ICollection<Election> CreatedElections { get; set; } = new List<Election>();

        // Relationship with Candidates added by the User
        public ICollection<Candidate> AddedCandidates { get; set; } = new List<Candidate>();
    }
}
