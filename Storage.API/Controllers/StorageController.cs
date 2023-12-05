using Insurance.API.Models;
using Insurance.API.Services;
using Microsoft.AspNetCore.Mvc;
using Storage.API.Services;

namespace Storage.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;
        private readonly IInsuranceService _insuranceService;
        public StorageController(IStorageService storageService, IInsuranceService insuranceService)
        {
            _storageService = storageService;
            _insuranceService = insuranceService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var insurances = _insuranceService.GetInsuranceItems();

                if (insurances == null)
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
        public IActionResult Post([FromBody] InsuranceItem item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var insurance = _insuranceService.Add(item);
                return CreatedAtAction("Get", new { id = insurance.Id }, insurance);
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
                //if (!ModelState.IsValid)
                //    return BadRequest(ModelState);

                //var insuranceUpdate = _insuranceService.GetById(id);

                //if (insuranceUpdate == null)
                //    return NotFound($"Insurance with Id = '{id} not found.");

                //insuranceUpdate.FirstName = item.FirstName;
                //insuranceUpdate.LastName = item.LastName;

                //_insuranceService.Update(insuranceUpdate);
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
                //var insurace = _insuranceService.GetById(id);

                //if (insurace == null)
                //    return NotFound($"Insurance with Id = '{id}' not found.");

                //_insuranceService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
