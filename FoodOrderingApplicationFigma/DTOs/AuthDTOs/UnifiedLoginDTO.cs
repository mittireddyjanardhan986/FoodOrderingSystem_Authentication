namespace FoodOrderingApplicationFigma.DTOs.AuthDTOs
{
    public class UnifiedLoginDTO
    {
        public string? Identifier { get; set; }  // Email or Phone
        public string? Password { get; set; }
        public string? Otp { get; set; }
        public string? Token { get; set; }  // JWT token from send-otp
    }
}
