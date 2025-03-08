using System.ComponentModel.DataAnnotations;
using ElectionSystem.Dtos;
namespace ElectionSystem.Dtos
{
    public class ResultDto
    {
        [Required(ErrorMessage = "معرف المرشح مطلوب")]
        public int CandidateId { get; set; }

        [Required(ErrorMessage = "اسم المرشح مطلوب")]
        [StringLength(100, ErrorMessage = "اسم المرشح لا يجب أن يتجاوز 100 حرف")]
        public string CandidateName { get; set; } = string.Empty;

        [Required(ErrorMessage = "عدد الأصوات مطلوب")]
        [Range(0, int.MaxValue, ErrorMessage = "عدد الأصوات يجب أن يكون رقمًا صحيحًا غير سالب")]
        public int VoteCount { get; set; }

        [Required(ErrorMessage = "معرف الانتخابات مطلوب")]
        public int ElectionId { get; set; }

        [Required(ErrorMessage = "اسم الانتخابات مطلوب")]
        [StringLength(100, ErrorMessage = "اسم الانتخابات لا يجب أن يتجاوز 100 حرف")]
        public string ElectionName { get; set; } = string.Empty;
    }
}

