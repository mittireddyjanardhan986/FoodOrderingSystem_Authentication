namespace FoodOrderingApplicationFigma.DTOs.AdminDTOs
{
    public class GetAdminDTO
    {
        public int AdminId { get; set; }
        public int UserId { get; set; }
        public string? UserFullName { get; set; }
        public string? UserEmail { get; set; }
    }
}