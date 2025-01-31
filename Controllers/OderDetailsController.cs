using System.Data;
using System.Data.SqlClient;
using GetOrderDetails.Iservice;
using GetOrderDetails.Models;
using GetOrderDetails.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GetOrderDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OderDetailsController : ControllerBase
    {
        private readonly ICustomerOderDetails _customerOrderRepository;
        public OderDetailsController(ICustomerOderDetails customerOrderRepository)
        {
            _customerOrderRepository = customerOrderRepository;
        }

        [HttpPost("LatestOrderDetails")]
        public IActionResult GetLatestOrder(OderDetRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.User) || string.IsNullOrEmpty(request.CustomerId))
            {
                return BadRequest(new { message = "Invalid request data." });
            }

            try
            {
                var customerOrderResponse = _customerOrderRepository.GetLatestOrderDetails(request.User, request.CustomerId);
                if (customerOrderResponse == null)
                {
                    return NotFound(new { message = "No order found for this customer." });
                }
                return Ok(customerOrderResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }
    }

}

