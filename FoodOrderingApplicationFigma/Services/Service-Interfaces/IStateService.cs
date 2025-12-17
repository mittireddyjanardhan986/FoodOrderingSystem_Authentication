using FoodOrderingApplicationFigma.DTOs.StateDTOs;

namespace FoodOrderingApplicationFigma.Services.Service_Interfaces
{
    public interface IStateService
    {
        Task<IEnumerable<GetStateDTO>> GetAllStates();
        Task<GetStateDTO?> GetStateById(int id);
        Task<GetStateDTO> CreateState(CreateStateDTO createStateDTO);
        Task<GetStateDTO?> UpdateState(UpdateStateDTO updateStateDTO);
        Task<GetStateDTO> DeleteState(int id);
    }
}