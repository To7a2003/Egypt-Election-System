using System.ComponentModel.DataAnnotations;

namespace ElectionSystem.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صالح.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "كلمة المرور يجب أن تحتوي على 6 أحرف على الأقل.")]
        public string Password { get; set; } = string.Empty;
    }
}
