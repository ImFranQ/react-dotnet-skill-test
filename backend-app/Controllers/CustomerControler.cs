using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using MyApp.Services;

namespace backend_app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly CustomerService _customerService;

    public CustomerController(ILogger<CustomerController> logger, CustomerService service)
    {
        _logger = logger;
        _customerService = service;
    }

    /**
    * Get all customers
    */
    [HttpGet(Name = "GetCustomerList")]
    public IActionResult GetCustomerList([FromQuery] PaginationParams @params)
    {
        return Ok(_customerService.PaginatedCustomerList(@params));
    }


    /**
    * Get customer by id
    */
    [HttpGet("{id}", Name = "GetCustomer")]
    public IActionResult Get(int id)
    {
        var customer = _customerService.GetCustomer(id);
        if(customer == null) {
            return NotFound();
        }
        return Ok(customer);
    }

    /**
    * Create new customer
    */
    [HttpPost(Name = "AddCustomer")]
    public IActionResult Post([FromBody] Customer customer)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var added = _customerService.AddCustomer(customer);
        return Ok(added);
    }

    /**
    * Update customer
    */
    [HttpPut(Name = "UpdateCustomer")]
    public IActionResult Put([FromBody] Customer customer)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var updated = _customerService.UpdateCustomer(customer);
        return Ok(updated);
    }

    /**
    * Delete customer
    */
    [HttpDelete("{id}", Name = "DeleteCustomer")]
    public IActionResult Delete(int id)
    {
        if( _customerService.DeleteCustomer(id) ) {
            return Ok();
        }
        
        return NotFound();
    }

}