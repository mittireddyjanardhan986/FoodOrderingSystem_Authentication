namespace FoodOrderingApplicationFigma.Services.Service_Interfaces
{
    public interface ISmsService
    {
        Task SendOtpAsync(string phone, string otp);
    }
}
