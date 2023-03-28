using CustomerService.BL;
using CustomerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IMongoActions _mongoActions;
        public CustomerController(IMongoActions mongoActions) 
        {
            _mongoActions = mongoActions;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Customer> customers =await  _mongoActions.GetCustomers();
                return new OkObjectResult(customers);
            }
            catch (Exception ex) 
            {
                //log here
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                Customer customer = await _mongoActions.GetCustomerById(id);
                if (customer == null)
                    return NotFound();
                return new OkObjectResult(customer);
            }
            catch (Exception ex)
            {
                //log here
                return new NotFoundResult();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Customer customer)
        {
            try
            {
                await _mongoActions.InsertCustomer(customer);
                return Ok();
            }
            catch (Exception ex)
            {
                //log here
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(Customer customer)
        {
            try
            {
                var isUpdated = await _mongoActions.UpdateCustomer(customer);
                if(!isUpdated)
                    return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                //log here
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var isDeleted = await _mongoActions.DeleteCustomer(id);
                if (!isDeleted)
                    return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                //log here
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
