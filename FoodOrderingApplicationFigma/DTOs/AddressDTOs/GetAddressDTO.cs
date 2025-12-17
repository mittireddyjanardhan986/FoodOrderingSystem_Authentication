using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.AddressDTOs
{
    public class GetAddressDTO
    {
        [Required]
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string? AddressLine { get; set; }
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public int StateId { get; set; }
        public string? StateName { get; set; }
        public bool? IsDefault { get; set; }
    }
}