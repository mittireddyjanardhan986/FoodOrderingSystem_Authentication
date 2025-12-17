using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.AddressDTOs
{
    public class UpdateAddressDTO
    {
        [Required]
        public int AddressId { get; set; }
        public int? UserId { get; set; }
        public string? AddressLine { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public bool? IsDefault { get; set; }
    }
}