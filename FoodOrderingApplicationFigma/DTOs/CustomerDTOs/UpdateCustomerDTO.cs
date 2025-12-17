using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.CustomerDTOs
{
    public class UpdateCustomerDTO
    {
        [Required]
        public long CustomerId { get; set; }
        public int? UserId { get; set; }
    }
}