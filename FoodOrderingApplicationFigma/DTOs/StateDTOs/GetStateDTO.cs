namespace FoodOrderingApplicationFigma.DTOs.StateDTOs
{
    public class GetStateDTO
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = "";
        public bool? IsActive { get; set; }
    }
}