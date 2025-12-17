using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FoodOrderingApplicationFigma.DTOs.AuthDTOs
{
    public class RegisterRestaurantDTO
    {
        [Required]
        public string RestaurantName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int StateId { get; set; }
    }
}
