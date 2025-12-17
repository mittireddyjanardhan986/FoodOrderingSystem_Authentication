using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.CommonDTOs
{
    public class EmailRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}