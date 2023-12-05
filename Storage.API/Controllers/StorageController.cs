using Microsoft.AspNetCore.Mvc;
using Storage.API.Models;
using Storage.API.Services;

namespace Storage.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class StorageController : ControllerBase
    {
        #region Variables
        private readonly IStorageService _storageService;
        #endregion
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="storageService">IStorageService instance.</param>
        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }
        /// <summary>
        /// The method that returns all storage items from the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public async Task<ActionResult<StorageItem>> Get()
        {
            try
            {
                var insurances = await _storageService.GetStorageItems();

                if (insurances.Count() == 0)
                    return NotFound("Insurances not found.");

                return Ok(insurances);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// The method that returns the storage item by guidId from the database.
        /// </summary>
        /// <param name="guidId">GuidID parameter.</param>
        /// <returns></returns>
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
        /// <summary>
        /// The method that adds new storage item in the database.
        /// </summary>
        /// <param name="item">StorageItem instance.</param>
        /// <returns></returns>
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
        /// <summary>
        /// The method that updates the storage item in the database.
        /// </summary>
        /// <param name="item">StorageItem instance.</param>
        /// <param name="guidId">GuidID parameter.</param>
        /// <returns></returns>
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
        /// <summary>
        /// The method that deletes the storage item from the database.
        /// </summary>
        /// <param name="guidId">GuidID parameter.</param>
        /// <returns></returns>
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
