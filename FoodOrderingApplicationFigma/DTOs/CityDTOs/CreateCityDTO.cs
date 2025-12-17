using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.CityDTOs
{
    public class CreateCityDTO
    {
        [StringLength(100)]
        [Required]
        public string CityName { get; set; } = "";
        [Required]
        public int StateId { get; set; }
        [Required]
        public bool? IsActive { get; set; }
    }
}