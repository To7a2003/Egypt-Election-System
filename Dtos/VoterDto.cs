using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ElectionSystem.Dtos
{
    public class VoterDto
    {
        [Required(ErrorMessage = "Voter name is required")]
        [StringLength(100, ErrorMessage = "Voter name must not exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "National ID is required")]
        [StringLength(14, ErrorMessage = "National ID must be exactly 14 digits")]
        public string NationalId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Election type is required")]
        public ElectionType ElectionType { get; set; }

        [MaxLength(255, ErrorMessage = "Path must not exceed 255 characters")]
        public string SelfiePath { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Path must not exceed 255 characters")]
        public string IDCardPath { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "صورة البطاقة مطلوبة.")]
        public byte[]? NationalIdPhoto { get; set; } // صورة بطاقة الناخب (Nullable)

        [Required(ErrorMessage = "صورة السيلفي مطلوبة.")]
        public byte[]? SelfiePhoto { get; set; } // صورة السيلفي (Nullable)
    }
}
