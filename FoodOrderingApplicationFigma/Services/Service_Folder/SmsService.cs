using FoodOrderingApplicationFigma.Services.Service_Interfaces;

namespace FoodOrderingApplicationFigma.Services.Service_Folder
{
    public class SmsService : ISmsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "a48b1f31-d523-11f0-a6b2-0200cd936042";

        public SmsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendOtpAsync(string phone, string otp)
        {
            string url = $"https://2factor.in/API/V1/{_apiKey}/SMS/{phone}/{otp}/FoodDeliveryApp";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}
