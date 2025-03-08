using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Candidate
    {
        [Key] // المفتاح الأساسي
        public int Id { get; set; }

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

        
        // العلاقة مع المستخدم الذي أضاف المرشح
        [ForeignKey("AddedByUser")]
      
        public int AddedByUserId { get; set; }
        
        public virtual User AddedByUser { get; set; } = null!; // إعطاء قيمة افتراضية لتجنب null

        // العلاقة مع الانتخابات
        [ForeignKey("Election")]
        [Required(ErrorMessage = "الانتخابات المرتبطة مطلوبة")]
        public int ElectionId { get; set; }
        public Election Election { get; set; } = new Election(); // إعطاء قيمة افتراضية لتجنب null
    }
}
