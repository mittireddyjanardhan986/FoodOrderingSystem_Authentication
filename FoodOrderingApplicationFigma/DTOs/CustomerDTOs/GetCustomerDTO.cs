namespace FoodOrderingApplicationFigma.DTOs.CustomerDTOs
{
    public class GetCustomerDTO
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string? UserFullName { get; set; }
        public string? UserEmail { get; set; }
    }
}