using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.CommonDTOs
{
    public class IdRequestDTO
    {
        [Required]
        public int Id { get; set; }
    }
}