using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.OtpDTOs
{
    public class VerifyOtpRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Otp { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
