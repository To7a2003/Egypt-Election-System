using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Election
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الانتخابات مطلوب")]
        [StringLength(100, ErrorMessage = "اسم الانتخابات لا يجب أن يتجاوز 100 حرف")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "تاريخ البدء مطلوب")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "تاريخ الانتهاء مطلوب")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "نوع الانتخابات مطلوب")]
        public ElectionType ElectionType { get; set; }

        // العلاقة مع المحافظات
        public List<Province>? Provinces { get; set; }

        // العلاقة مع المرشحين
        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

        // العلاقة مع المشرف
        public int? SupervisorId { get; set; }
        public User? Supervisor { get; set; }

        // العلاقة مع المستخدم الذي أنشأ الانتخابات
        public int? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }
    }

    public enum ElectionType
    {
        Presidential,  // انتخابات رئاسية
        Parliament,    // مجلس الشعب
        Senate         // مجلس الشيوخ
    }
}

