using System.ComponentModel.DataAnnotations;
using Core.Entities;
using ElectionSystem.Models; // تأكد أن الـ namespace هنا يتطابق مع المسار الصحيح

namespace ElectionSystem.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صالح.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "كلمة المرور يجب أن تحتوي على 6 أحرف على الأقل.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "الاسم مطلوب.")]
        [StringLength(100, ErrorMessage = "الاسم لا يجب أن يتجاوز 100 حرف.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "الرقم القومي مطلوب.")]
        [StringLength(14, ErrorMessage = "الرقم القومي يجب أن يتكون من 14 رقمًا.")]
        public string NationalId { get; set; } = string.Empty;

        [Required(ErrorMessage = "تاريخ الميلاد مطلوب.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "نوع الانتخابات مطلوب.")]
        public ElectionType ElectionType { get; set; }

        [Required(ErrorMessage = "صورة البطاقة مطلوبة.")]
        public byte[]? NationalIdPhoto { get; set; } // صورة بطاقة الناخب (Nullable)

        [Required(ErrorMessage = "صورة السيلفي مطلوبة.")]
        public byte[]? SelfiePhoto { get; set; } // صورة السيلفي (Nullable)

        // تعيين الدور بشكل افتراضي بناءً على البريد الإلكتروني (يمكن تعديلها لاحقًا)
        public string Role { get; set; } = "Voter"; // يمكنك تخصيص الدور بناءً على الـ email أو شروط أخرى
    }
}

