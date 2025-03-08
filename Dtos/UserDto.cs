using System.ComponentModel.DataAnnotations;

namespace ElectionSystem.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صالح")]
        [StringLength(100, ErrorMessage = "البريد الإلكتروني لا يجب أن يتجاوز 100 حرف")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [StringLength(100, ErrorMessage = "كلمة المرور لا يجب أن تتجاوز 100 حرف")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "الدور مطلوب")]
        public string Role { get; set; } = string.Empty;

    }
}
