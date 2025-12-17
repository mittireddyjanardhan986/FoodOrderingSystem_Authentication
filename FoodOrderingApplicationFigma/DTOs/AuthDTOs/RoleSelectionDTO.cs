namespace FoodOrderingApplicationFigma.DTOs.AuthDTOs
{
    public class RoleSelectionDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public int Role { get; set; }
        public string RoleName { get; set; } = "";
    }
}