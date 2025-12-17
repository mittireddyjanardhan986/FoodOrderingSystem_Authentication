using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.AuthDTOs
{
    public class RegisterDTO
    {
        [Required]
        public string FullName { get; set; } = "";
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        
        [Required]
        public string Phone { get; set; } = "";
        
        [Required]
        public string Password { get; set; } = "";
        
        [Required]
        public int Role { get; set; }
    }
}