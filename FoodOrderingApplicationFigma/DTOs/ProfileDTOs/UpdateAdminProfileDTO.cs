using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.ProfileDTOs
{
    public class UpdateAdminProfileDTO
    {
        [Required]
        public int UserId { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
    }
}
