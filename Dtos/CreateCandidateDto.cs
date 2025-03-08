using System.ComponentModel.DataAnnotations;

namespace ElectionSystem.Dtos
{
    public class CreateCandidateDto
    {
        [Required(ErrorMessage = "اسم المرشح مطلوب")]
        [MaxLength(50, ErrorMessage = "اسم المرشح يجب أن لا يزيد عن 50 حرفاً")]
        public string Name { get; set; } = string.Empty;  // إعطاء قيمة افتراضية لتجنب التحذير


        [MaxLength(50, ErrorMessage = "اسم الحزب يجب أن لا يزيد عن 50 حرفاً")]
        public string? Party { get; set; } // اسم الحزب (اختياري)

        public string? Bio { get; set; } // نبذة عن المرشح (اختياري)

        [Required]
        public int VoteCount { get; set; } = 0; // عدد الأصوات (الحقل الجديد)

        [MaxLength(200)]
        public string? ImagePath { get; set; } // مسار الصور

    }
}
