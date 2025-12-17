using AutoMapper;
using FoodOrderingApplicationFigma.DTOs.AddressDTOs;
using FoodOrderingApplicationFigma.DTOs.AdminDTOs;
using FoodOrderingApplicationFigma.DTOs.AuthDTOs;
using FoodOrderingApplicationFigma.DTOs.CityDTOs;
using FoodOrderingApplicationFigma.DTOs.CustomerDTOs;
using FoodOrderingApplicationFigma.DTOs.RestaurantDTOs;
using FoodOrderingApplicationFigma.DTOs.RoleDTOs;
using FoodOrderingApplicationFigma.DTOs.StateDTOs;
using FoodOrderingApplicationFigma.Models;

namespace FoodOrderingApplicationFigma.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Address mappings
            CreateMap<Address, GetAddressDTO>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.CityName))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.StateName));
            CreateMap<CreateAddressDTO, Address>();
            CreateMap<UpdateAddressDTO, Address>();

            // Admin mappings
            CreateMap<Admin, GetAdminDTO>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));
            CreateMap<CreateAdminDTO, Admin>();
            CreateMap<UpdateAdminDTO, Admin>();

            // City mappings
            CreateMap<City, GetCityDTO>()
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.StateName));
            CreateMap<CreateCityDTO, City>();
            CreateMap<UpdateCityDTO, City>();

            // State mappings
            CreateMap<State, GetStateDTO>();
            CreateMap<CreateStateDTO, State>();
            CreateMap<UpdateStateDTO, State>();

            // Customer mappings
            CreateMap<Customer, GetCustomerDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.customer_id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));
            CreateMap<CreateCustomerDTO, Customer>()
                .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src.UserId));
            CreateMap<UpdateCustomerDTO, Customer>()
                .ForMember(dest => dest.customer_id, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src.UserId));

            // Restaurant mappings
            CreateMap<Restaurant, GetRestaurantDTO>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.CityName))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.StateName));
            CreateMap<CreateRestaurantDTO, Restaurant>();
            CreateMap<UpdateRestaurantDTO, Restaurant>();

            // Role mappings
            CreateMap<Role, GetRoleDTO>();
            CreateMap<CreateRoleDTO, Role>();
            CreateMap<UpdateRoleDTO, Role>();

            // Auth mappings
            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
        }
    }
}