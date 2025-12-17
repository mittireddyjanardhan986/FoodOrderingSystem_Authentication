using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.AuthDTOs
{
    public class LoginWithRoleDTO
    {
        [Required]
        public string Identifier { get; set; } = "";
        
        public string? Password { get; set; }
        
        public string? Otp { get; set; }
        
        public string? Token { get; set; }
        
        [Required]
        public int SelectedUserId { get; set; }
    }
}