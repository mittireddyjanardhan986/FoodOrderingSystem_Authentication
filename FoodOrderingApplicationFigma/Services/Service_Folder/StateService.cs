using AutoMapper;
using FoodOrderingApplicationFigma.DTOs.StateDTOs;
using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Repository.Repository_Interface;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;

namespace FoodOrderingApplicationFigma.Services.Service_Folder
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public StateService(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetStateDTO>> GetAllStates()
        {
            var states = await _stateRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<GetStateDTO>>(states);
        }

        public async Task<GetStateDTO?> GetStateById(int id)
        {
            var state = await _stateRepository.GetUserById(id);
            if (state == null) throw new Exception("State Not Found");
            return _mapper.Map<GetStateDTO>(state);
        }

        public async Task<GetStateDTO> CreateState(CreateStateDTO createStateDTO)
        {
            var state = _mapper.Map<State>(createStateDTO);
            var createdState = await _stateRepository.InsertUser(state);
            return _mapper.Map<GetStateDTO>(createdState);
        }

        public async Task<GetStateDTO?> UpdateState(UpdateStateDTO updateStateDTO)
        {
            var existing = await _stateRepository.GetUserById(updateStateDTO.StateId);
            if (existing == null) throw new Exception("State Not Found");
            
            var state = _mapper.Map<State>(updateStateDTO);
            var updatedState = await _stateRepository.UpdateUser(state);
            return _mapper.Map<GetStateDTO>(updatedState);
        }

        public async Task<GetStateDTO> DeleteState(int id)
        {
            var state = await _stateRepository.GetUserById(id);
            if (state == null) throw new Exception("State Not Found");
            
            await _stateRepository.DeleteUser(id);
            return _mapper.Map<GetStateDTO>(state);
        }
    }
}