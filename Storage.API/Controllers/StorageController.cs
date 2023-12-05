using Microsoft.AspNetCore.Mvc;
using Storage.API.Models;
using Storage.API.Services;

namespace Storage.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;
        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var insurances = _storageService.GetStorageItems();

                if (insurances == null)
                    return NotFound("Insurances not found.");

                return Ok(insurances);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{guidId}")]
        public IActionResult Get(Guid guidId)
        {
            try
            {
                var insurance = _storageService.GetById(guidId);

                if (insurance == null)
                    return NotFound($"Insurance with Id = '{guidId} not found.");

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

                var insurance = _storageService.Add(item);
                return CreatedAtAction("Get", new { id = insurance.Id }, insurance);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put([FromBody] StorageItem item, Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var insuranceUpdate = _storageService.GetById(id);

                if (insuranceUpdate == null)
                    return NotFound($"Insurance with Id = '{id} not found.");

                insuranceUpdate.Result.FirstName = item.FirstName;
                insuranceUpdate.Result.LastName = item.LastName;

                _storageService.Update(insuranceUpdate.Result);
                return Content($"Insurance with Id = '{id}' is updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
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
    }
}
