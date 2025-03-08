using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectionSystem.Models;  // Add this line to include the namespace for Voter

namespace Core.Entities
{
    public class Province
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم المحافظة مطلوب")]
        [StringLength(100, ErrorMessage = "اسم المحافظة لا يجب أن يتجاوز 100 حرف")]
        public string? Name { get; set; }

        [ForeignKey("Election")]
        public int? ElectionId { get; set; }
        public Election? Election { get; set; }

        public List<Voter> Voters { get; set; } = new List<Voter>();  // Now this works because 'Voter' is correctly referenced
    }
}
