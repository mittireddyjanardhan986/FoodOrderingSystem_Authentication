using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.AddressDTOs
{
    public class CreateAddressDTO
    {
        [Required]
        public int UserId { get; set; }
        [RegularExpression(@"^[A-Z][a-zA-Z\s]{2,}$", ErrorMessage = "Address must start with a capital letter and contain only letters and spaces")]
        [StringLength(100)]
        public string? AddressLine { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public bool? IsDefault { get; set; }
    }
}