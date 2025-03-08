using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ElectionSystem.Dtos
{
    public class ElectionDto
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
    }
}
