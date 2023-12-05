using Insurance.API.Services;
using Insurance.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Storage.API.Services;
using Storage.API.Models;

namespace Insurance.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;
        private readonly IStorageService _storageService;
        public InsuranceController(IInsuranceService service, IStorageService storageService)
        {
            _insuranceService = service;
            _storageService = storageService;
        }
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        var insurances = _insuranceService.GetInsuranceItems();

        //        if (insurances == null)
        //            return NotFound("Insurances not found.");

        //        return Ok(insurances);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        //[HttpGet("{id}")]
        //public IActionResult Get(Guid id)
        //{
        //    try
        //    {
        //        var insurance = _insuranceService.GetById(id);

        //        if (insurance == null)
        //            return NotFound($"Insurance with Id = '{id} not found.");

        //        return Ok(insurance);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        //[HttpPost]
        //public IActionResult Post([FromBody] InsuranceItem item)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        if (ValidateCustomerNumber(item.CustomerNumber))
        //        {
        //            var insurance = _insuranceService.Add(item);
        //            return CreatedAtAction("Get", new { id = insurance.Id }, insurance);
        //        }
        //        else
        //            return BadRequest($"Check that the provided customer number = '{item.CustomerNumber}' is correc.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        //[HttpPut]
        //public IActionResult Put([FromBody] InsuranceItem item, Guid id)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        var insuranceUpdate = _insuranceService.GetById(id);

        //        if (insuranceUpdate == null)
        //            return NotFound($"Insurance with Id = '{id} not found.");

        //        insuranceUpdate.FirstName = item.FirstName;
        //        insuranceUpdate.LastName = item.LastName;

        //        _insuranceService.Update(insuranceUpdate);
        //        return Content($"Insurance with Id = '{id}' is updated.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    try
        //    {
        //        var insurace = _insuranceService.GetById(id);

        //        if (insurace == null)
        //            return NotFound($"Insurance with Id = '{id}' not found.");

        //        _insuranceService.Delete(id);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        //private static bool ValidateCustomerNumber(string customerNumber)
        //{
        //    if (customerNumber == null)
        //        return false;

        //    if (customerNumber.Length == 10 || customerNumber.Length == 12)
        //        return true;

        //    return false;
        //}
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var insurances = _storageService.GetStorageItems();

                if (insurances.Result.Count() == 0)
                    return NotFound("Insurances not found.");

                return Ok(insurances);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var insurance = _insuranceService.GetById(id);

                if (insurance == null)
                    return NotFound($"Insurance with Id = '{id} not found.");

                return Ok(insurance);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] StorageItem item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (ValidateCustomerNumber(item.CustomerNumber))
                {
                    var insurance = _storageService.Add(item);
                    return CreatedAtAction("Get", new { id = insurance.Id }, insurance);
                }
                else
                    return BadRequest($"Check that the provided customer number = '{item.CustomerNumber}' is correct.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put([FromBody] InsuranceItem item, Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var insuranceUpdate = _insuranceService.GetById(id);

                if (insuranceUpdate == null)
                    return NotFound($"Insurance with Id = '{id} not found.");

                insuranceUpdate.FirstName = item.FirstName;
                insuranceUpdate.LastName = item.LastName;

                _insuranceService.Update(insuranceUpdate);
                return Content($"Insurance with Id = '{id}' is updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var insurace = _insuranceService.GetById(id);

                if (insurace == null)
                    return NotFound($"Insurance with Id = '{id}' not found.");

                _insuranceService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        private static bool ValidateCustomerNumber(string customerNumber)
        {
            if (customerNumber == null)
                return false;

            if (customerNumber.Length == 10 || customerNumber.Length == 12)
                return true;

            return false;
        }
    }
}
