using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.CustomerDTOs
{
    public class CreateCustomerDTO
    {
        [Required]
        public int UserId { get; set; }
    }
}