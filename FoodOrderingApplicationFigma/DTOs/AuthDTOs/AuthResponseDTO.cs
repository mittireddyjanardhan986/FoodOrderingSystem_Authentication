namespace FoodOrderingApplicationFigma.DTOs.AuthDTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = "";
        public string Email { get; set; } = "";
        public string FullName { get; set; } = "";
        public int Role { get; set; }
        public DateTime Expiration { get; set; }
    }
}