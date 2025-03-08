using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ElectionSystem.Models
{
    public class Voter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Voter name is required")]
        [StringLength(100, ErrorMessage = "Voter name must not exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "National ID is required")]
        [StringLength(14, ErrorMessage = "National ID must be exactly 14 digits")]
        public string NationalId { get; set; } = string.Empty;

        private string province = string.Empty; // Default value to avoid null

        public string GetProvince()
        {
            return province;
        }

        public void SetProvince(string value)
        {
            province = value;
        }

        [Required(ErrorMessage = "Election type is required")]
        public ElectionType ElectionType { get; set; }

        [MaxLength(255, ErrorMessage = "Path must not exceed 255 characters")]
        public string SelfiePath { get; set; } = string.Empty; // Default value to avoid null

        [MaxLength(255, ErrorMessage = "Path must not exceed 255 characters")]
        public string IDCardPath { get; set; } = string.Empty; // Default value to avoid null

        public bool HasVoted { get; set; } = false; // Default value False

        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "صورة البطاقة مطلوبة.")]
        public byte[]? NationalIdPhoto { get; set; } // صورة بطاقة الناخب (Nullable)

        [Required(ErrorMessage = "صورة السيلفي مطلوبة.")]
        public byte[]? SelfiePhoto { get; set; } // صورة السيلفي (Nullable)
        // Relation with User
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int ProvinceId { get; set; } // Foreign key
        public Province Province { get; set; } = null!;
    }
}
