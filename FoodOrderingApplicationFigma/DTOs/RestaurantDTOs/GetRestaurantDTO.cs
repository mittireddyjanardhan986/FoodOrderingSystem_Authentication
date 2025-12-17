namespace FoodOrderingApplicationFigma.DTOs.RestaurantDTOs
{
    public class GetRestaurantDTO
    {
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        public string? UserFullName { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public int StateId { get; set; }
        public string? StateName { get; set; }
        public string Status { get; set; } = "";
        public DateTime? CreatedAt { get; set; }
    }
}