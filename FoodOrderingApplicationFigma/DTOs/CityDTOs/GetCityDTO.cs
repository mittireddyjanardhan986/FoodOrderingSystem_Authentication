namespace FoodOrderingApplicationFigma.DTOs.CityDTOs
{
    public class GetCityDTO
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = "";
        public int StateId { get; set; }
        public string? StateName { get; set; }
        public bool? IsActive { get; set; }
    }
}