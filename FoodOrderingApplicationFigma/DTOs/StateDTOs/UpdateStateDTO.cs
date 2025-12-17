using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.StateDTOs
{
    public class UpdateStateDTO
    {
        [Required]
        public int StateId { get; set; }
        public string? StateName { get; set; } = "";
        public bool? IsActive { get; set; }
    }
}