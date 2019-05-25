using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Common;
using DataAccess.POCO;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/customers/")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly ICustomersService _service;

        public AddressesController(ICustomersService service)
        {
            _service = service;
        }

        [HttpGet("{id}/addresses")]
        public ActionResult GetAddresses(string id)
        {
            var serviceResult = _service.GetCustomerAddresses(id);
            if (serviceResult.Status == eOperationStatus.Success)
            {
                return Ok(serviceResult);
            }

            if (serviceResult.Status == eOperationStatus.NotFound)
            {
                return NotFound(serviceResult.ExceptionMessage);
            }

            return BadRequest(serviceResult);
        }

        [HttpPost("{id}/addresses")]
        public ActionResult AddAddress(string id, [FromBody] Addresses address)
        {
            var serviceResult = _service.AddAddress(id, address);
            if (serviceResult.Status == eOperationStatus.Success)
            {
                return Ok(serviceResult);
            }

            if (serviceResult.Status == eOperationStatus.NotFound)
            {
                return NotFound(serviceResult.ExceptionMessage);
            }

            return BadRequest(serviceResult);
        }

        [HttpPut("{id}/addresses/{addrId}")]
        public ActionResult UpdateAddress(string id, string addrId, [FromBody] Addresses address)
        {
            address.AddressId = addrId;
            var serviceResult = _service.UpdateAddress(id, address);
            if (serviceResult.Status == eOperationStatus.Success)
            {
                return Ok(serviceResult);
            }

            if (serviceResult.Status == eOperationStatus.NotFound)
            {
                return NotFound(serviceResult.ExceptionMessage);
            }

            return BadRequest(serviceResult);
        }

        [HttpGet("{id}/addresses/{addrId}")]
        public ActionResult GetAddress(string id, string addrId)
        {
            var serviceResult = _service.GetAddress(id, addrId);
            if (serviceResult.Status == eOperationStatus.Success)
            {
                return Ok(serviceResult);
            }

            if (serviceResult.Status == eOperationStatus.NotFound)
            {
                return NotFound(serviceResult.ExceptionMessage);
            }

            return BadRequest(serviceResult);
        }

        [HttpDelete("{id}/addresses/{addrId}")]
        public ActionResult DeleteAddress(string id, string addrId)
        {
            var serviceResult = _service.DeleteAddress(id, addrId);
            if (serviceResult.Status == eOperationStatus.Success)
            {
                return Ok(serviceResult);
            }

            if (serviceResult.Status == eOperationStatus.NotFound)
            {
                return NotFound(serviceResult.ExceptionMessage);
            }

            return BadRequest(serviceResult);
        }
    }
}
