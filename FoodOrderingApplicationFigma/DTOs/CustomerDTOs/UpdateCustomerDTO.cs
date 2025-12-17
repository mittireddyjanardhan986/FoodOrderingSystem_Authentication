using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.CustomerDTOs
{
    public class UpdateCustomerDTO
    {
        [Required]
        public int CustomerId { get; set; }
        public int? UserId { get; set; }
    }
}