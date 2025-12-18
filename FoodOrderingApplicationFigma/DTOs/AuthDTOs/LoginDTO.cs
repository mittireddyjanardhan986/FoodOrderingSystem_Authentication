using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.AuthDTOs
{
    public class LoginDTO
    {
        [Required]
        public string Identifier { get; set; } = "";
        
        [Required]
        public string Password { get; set; } = "";
    }
}
