using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.CityDTOs
{
    public class UpdateCityDTO
    {
        [Required]
        public int CityId { get; set; }
        public string CityName { get; set; } = "";
        public int StateId { get; set; }
        public bool? IsActive { get; set; }
    }
}