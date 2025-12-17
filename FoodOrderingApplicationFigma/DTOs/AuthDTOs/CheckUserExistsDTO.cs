using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.AuthDTOs
{
    public class CheckUserExistsDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }
    }
}