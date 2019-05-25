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
        public ActionResult<string> Get(int id)
        {
            return "value";
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion
    }
}