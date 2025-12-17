using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.StateDTOs
{
    public class CreateStateDTO
    {
        [Required]
        public string StateName { get; set; } = "";
        [Required]
        public bool? IsActive { get; set; }
    }
}