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
        /// The method that returns the storage item by guidId from the database.
        /// </summary>
        /// <param name="insuranceGuid">GuidID parameter.</param>
        /// <param name="customerNumber">Customer number parameter.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<StorageItem>> Get(Guid insuranceGuid, string customerNumber)
        {
            try
            {
                var insurance = await _storageService.GetByIdAndCustomerNumber(insuranceGuid, customerNumber);

                if (insurance == null)
                    return NotFound($"Insurance with Id = '{insuranceGuid}' and Customer number = '{customerNumber}' not found.");

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
        /// <param name="insuranceGuid">GuidID parameter.</param>
        /// <param name="customerNumber">Customer number parameter.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<StorageItem>> Put([FromBody] StorageItem item, Guid insuranceGuid, string customerNumber)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var insuranceUpdate = await _storageService.GetByIdAndCustomerNumber(insuranceGuid, customerNumber);

                if (insuranceUpdate == null)
                    return NotFound($"Insurance with Id = '{insuranceGuid}' and Customer number = '{customerNumber}' not found.");

                insuranceUpdate.FirstName = item.FirstName;
                insuranceUpdate.LastName = item.LastName;

                await _storageService.Update(insuranceUpdate);
                return Content($"Insurance with Id = '{insuranceGuid}' and Customer number = '{customerNumber}' is updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// The method that deletes the storage item from the database.
        /// </summary>
        /// <param name="insuranceGuid">GuidID parameter.</param>
        /// <param name="customerNumber">Customer number parameter.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<StorageItem>> Delete(Guid insuranceGuid, string customerNumber)
        {
            try
            {
                var insurance = await _storageService.GetByIdAndCustomerNumber(insuranceGuid, customerNumber);

                if (insurance == null)
                    return NotFound($"Insurance with Id = '{insuranceGuid}' and Customer number = '{customerNumber}' not found.");

                _storageService.Delete(insuranceGuid, customerNumber);
                return Content($"Insurance with Id = '{insuranceGuid}' and Customer number = '{customerNumber}' was deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
