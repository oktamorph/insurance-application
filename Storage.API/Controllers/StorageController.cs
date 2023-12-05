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
        public async Task<ActionResult<StorageItem>> Get()
        {
            try
            {
                var insurances = await _storageService.GetStorageItems();

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
        public async Task<ActionResult<StorageItem>> Get(Guid guidId)
        {
            try
            {
                var insurance = await _storageService.GetById(guidId);

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
        public async Task<ActionResult<StorageItem>> Post([FromBody] StorageItem item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var insurance = await _storageService.Add(item);
                return CreatedAtAction("Get", new { id = insurance.Id }, insurance);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult<StorageItem>> Put([FromBody] StorageItem item, Guid guidId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var insuranceUpdate = await _storageService.GetById(guidId);

                if (insuranceUpdate == null)
                    return NotFound($"Insurance with Id = '{guidId} not found.");

                insuranceUpdate.FirstName = item.FirstName;
                insuranceUpdate.LastName = item.LastName;

                await _storageService.Update(insuranceUpdate);
                return Content($"Insurance with Id = '{guidId}' is updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{guidId}")]
        public async Task<ActionResult<StorageItem>> Delete(Guid guidId)
        {
            try
            {
                var insurance = await _storageService.GetById(guidId);

                if (insurance == null)
                    return NotFound($"Insurance with Id = '{guidId}' not found.");

                _storageService.Delete(guidId);
                return Content($"Insurance with Id = '{guidId}' was deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
