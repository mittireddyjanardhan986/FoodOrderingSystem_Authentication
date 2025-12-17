using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.ProfileDTOs
{
    public class VerifyAndUpdateContactDTO
    {
        [Required]
        public int UserId { get; set; }

        public string? NewEmail { get; set; }
        public string? NewPhone { get; set; }

        [Required]
        public string Otp { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
