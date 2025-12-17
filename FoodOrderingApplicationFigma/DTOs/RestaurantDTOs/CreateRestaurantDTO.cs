using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.RestaurantDTOs
{
    public class CreateRestaurantDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required, StringLength(100,MinimumLength =3,ErrorMessage ="Minimum length is 3 and maximum length is 100 characters")]
        [RegularExpression(@"^[A-Z][a-zA-Z\s]{2,}$", ErrorMessage = "Name must start with a capital letter and contain only letters and spaces")]
        public string Name { get; set; } = "";
        [RegularExpression(@"^[A-Z][a-zA-Z\s]{2,}$", ErrorMessage = "Description must start with a capital letter and contain only letters and spaces")]
        public string? Description { get; set; }
        public string? Address { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public string Status { get; set; } = "";
        public DateTime? CreatedAt { get; set; }= DateTime.UtcNow;
    }
}