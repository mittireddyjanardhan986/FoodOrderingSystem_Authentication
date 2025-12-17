using FoodOrderingApplicationFigma.Attributes;
using FoodOrderingApplicationFigma.DTOs.AddressDTOs;
using FoodOrderingApplicationFigma.DTOs.CommonDTOs;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApplicationFigma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [AuthorizeRole(4)]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("getaddress")]
        public async Task<ActionResult<IEnumerable<GetAddressDTO>>> GetAllAddresses()
        {
            try
            {
                var addresses = await _addressService.GetAllAddresses();
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetAddressDTO>> GetAddressById([FromBody] IdRequestDTO dto)
        {
            try
            {
                var address = await _addressService.GetAddressById(dto.Id);
                return Ok(address);
            }
            catch (Exception ex) when (ex.Message == "Address Not Found")
            {
                return NotFound(new { message = "Address not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetAddressDTO>> CreateAddress([FromBody] CreateAddressDTO createAddressDTO)
        {
            try
            {
                var address = await _addressService.CreateAddress(createAddressDTO);
                return Ok(address);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<GetAddressDTO>> UpdateAddress([FromBody] UpdateAddressDTO updateAddressDTO)
        {
            try
            {
                var address = await _addressService.UpdateAddress(updateAddressDTO);
                return Ok(address);
            }
            catch (Exception ex) when (ex.Message == "Address Not Found")
            {
                return NotFound(new { message = "Address not found for update." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<GetAddressDTO>> DeleteAddress([FromBody] IdRequestDTO dto)
        {
            try
            {
                var deletedAddress = await _addressService.DeleteAddress(dto.Id);
                return Ok(deletedAddress);
            }
            catch (Exception ex) when (ex.Message == "Address Not Found")
            {
                return NotFound(new { message = "Address not found for deletion." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("get-by-user")]
        public async Task<ActionResult<IEnumerable<GetAddressDTO>>> GetAddressesByUserId([FromBody] IdRequestDTO dto)
        {
            try
            {
                var addresses = await _addressService.GetAddressesByUserId(dto.Id);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}