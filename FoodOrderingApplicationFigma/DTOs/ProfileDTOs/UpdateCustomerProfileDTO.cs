using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.ProfileDTOs
{
    public class UpdateCustomerProfileDTO
    {
        [Required]
        public int UserId { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
    }
}
