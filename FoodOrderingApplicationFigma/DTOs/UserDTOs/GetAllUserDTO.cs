using FoodOrderingApplicationFigma.DTOs.AddressDTOs;
using FoodOrderingApplicationFigma.DTOs.SampleDTOs;
using FoodOrderingApplicationFigma.Models;

namespace FoodOrderingApplicationFigma.DTOs
{
    public class GetAllUserDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public byte[] password { get; set; }
        public string RoleName { get; set; } = "";
        public DateTime? CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public IEnumerable<GetAddressDTO>? Addresses { get; set; }
        public IEnumerable<CartDTO>? Carts { get; set; }
        public IEnumerable<OrderDTO>? Orders { get; set; }
    }
}
