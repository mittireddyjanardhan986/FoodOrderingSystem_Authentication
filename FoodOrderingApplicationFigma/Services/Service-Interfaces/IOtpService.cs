namespace FoodOrderingApplicationFigma.Services.Service_Interfaces
{
    public interface IOtpService
    {
        string GenerateOtp();
        string GenerateOtpToken(string otp, string identifier);
        bool ValidateOtpToken(string token, string otp, string identifier);
    }
}
