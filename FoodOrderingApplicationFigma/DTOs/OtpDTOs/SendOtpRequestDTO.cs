using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.OtpDTOs
{
    public class SendOtpRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
