using System.ComponentModel.DataAnnotations;

namespace ElectionSystem.Dtos
{
    public class VoteDto
    {
        [Required(ErrorMessage = "معرف المرشح مطلوب")]
        public int CandidateId { get; set; }

        [Required(ErrorMessage = "معرف الانتخابات مطلوب")]
        public int ElectionId { get; set; }

        [Required(ErrorMessage = "معرف الناخب مطلوب")]
        public int VoterId { get; set; }
    }
}

