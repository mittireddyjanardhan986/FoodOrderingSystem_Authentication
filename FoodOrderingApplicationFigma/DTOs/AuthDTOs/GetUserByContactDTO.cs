using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.AuthDTOs
{
    public class GetUserByContactDTO
    {
        [Required]
        public string EmailOrPhone { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }
    }
}