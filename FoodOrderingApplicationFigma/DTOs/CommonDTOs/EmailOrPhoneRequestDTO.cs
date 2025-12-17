using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.CommonDTOs
{
    public class EmailOrPhoneRequestDTO
    {
        [Required]
        public string EmailOrPhone { get; set; } = "";
    }
}