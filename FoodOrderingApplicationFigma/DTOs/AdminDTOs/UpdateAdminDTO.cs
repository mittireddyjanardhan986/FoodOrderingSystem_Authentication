using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.AdminDTOs
{
    public class UpdateAdminDTO
    {
        [Required]
        public int AdminId { get; set; }
        public int? UserId { get; set; }
    }
}