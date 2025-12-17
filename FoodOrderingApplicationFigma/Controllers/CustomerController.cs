using FoodOrderingApplicationFigma.DTOs.CustomerDTOs;
using FoodOrderingApplicationFigma.DTOs.CommonDTOs;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FoodOrderingApplicationFigma.Attributes;

namespace FoodOrderingApplicationFigma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [AuthorizeRole(1)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCustomerDTO>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1, 4, 2)]
        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetCustomerDTO>> GetCustomerById([FromBody] IdRequestDTO dto)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(dto.Id);
                return Ok(customer);
            }
            catch (Exception ex) when (ex.Message == "Customer Not Found")
            {
                return NotFound(new { message = "Customer not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [AuthorizeRole(1, 4, 2)]
        [HttpPut]
        public async Task<ActionResult<GetCustomerDTO>> UpdateCustomer([FromBody] UpdateCustomerDTO updateCustomerDTO)
        {
            try
            {
                var customer = await _customerService.UpdateCustomer(updateCustomerDTO);
                return Ok(customer);
            }
            catch (Exception ex) when (ex.Message == "Customer Not Found")
            {
                return NotFound(new { message = "Customer not found for update." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<GetCustomerDTO>> DeleteCustomer([FromBody] IdRequestDTO dto)
        {
            try
            {
                var deletedCustomer = await _customerService.DeleteCustomer(dto.Id);
                return Ok(deletedCustomer);
            }
            catch (Exception ex) when (ex.Message == "Customer Not Found")
            {
                return NotFound(new { message = "Customer not found for deletion." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}