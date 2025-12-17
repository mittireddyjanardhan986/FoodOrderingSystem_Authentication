using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.RestaurantDTOs
{
    public class UpdateRestaurantDTO
    {
        [Required]
        public int RestaurantId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? Address { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public string? Status { get; set; } = "";
    }
}