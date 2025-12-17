using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApplicationFigma.DTOs.AdminDTOs
{
    public class CreateAdminDTO
    {
        [Required]
        public int UserId { get; set; }
    }
}