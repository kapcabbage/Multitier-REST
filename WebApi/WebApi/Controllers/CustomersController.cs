using System.Collections.Generic;
using System.Text.RegularExpressions;
using BusinessLogic.Interfaces;
using DataAccess.Common;
using DataAccess.POCO;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _service;

        public CustomersController(ICustomersService service)
        {
            _service = service;
        }
        #region Methods

        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            var serviceResult = _service.GetAllCustomers();
            if (serviceResult.Status == eOperationStatus.Success)
            {
                return Ok(serviceResult);
            }

            return NotFound();

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            var serviceResult = _service.GetCustomer(id);
            if (serviceResult.Status == eOperationStatus.Success)
            {
                return Ok(serviceResult);
            }

            if (serviceResult.Status == eOperationStatus.NotFound)
            {
                return NotFound();
            }

            return BadRequest(serviceResult);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] Customers value)
        {
            var result = _service.AddCustomer(value);
            if (result.Status == eOperationStatus.Success)
            {
                return Ok(new {Success = result.Data});
            }

            return BadRequest(new {Message = result.ExceptionMessage});
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Customers value)
        {
            value.CustomerId = id;
            var result = _service.UpdateCustomer(value);
            if (result.Status == eOperationStatus.Success)
            {
                return Ok(new { Success = result.Data });
            }

            if (result.Status == eOperationStatus.NotFound)
            {
                return NotFound();
            }

            return BadRequest(new { Message = result.ExceptionMessage });
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var result = _service.DeleteCustomer(id);
            if (result.Status == eOperationStatus.Success)
            {
                return Ok(new { Success = result.Data });
            }
            if (result.Status == eOperationStatus.NotFound)
            {
                return NotFound();
            }
            return BadRequest(new { Message = result.ExceptionMessage });
        }

        #endregion
    }
}