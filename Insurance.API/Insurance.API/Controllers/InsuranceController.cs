using Insurance.API.Services;
using Insurance.API.Models;
using Microsoft.AspNetCore.Mvc;
using Storage.API.Services;
using Storage.API.Models;

namespace Insurance.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class InsuranceController : ControllerBase
    {
        #region Variables
        private readonly IInsuranceService _insuranceService;
        private readonly IStorageService _storageService;
        #endregion
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="insuranceService">IInsuranceService instance.</param>
        /// <param name="storageService">IStorageService instance.</param>
        public InsuranceController(IInsuranceService insuranceService, IStorageService storageService)
        {
            _insuranceService = insuranceService;
            _storageService = storageService;
        }
        /// <summary>
        /// The method that returns the storage item by guidId from the database.
        /// </summary>
        /// <param name="insuranceGuid">GuidID parameter.</param>
        /// <param name="customerNumber">Customer number parameter.</param>
        [HttpGet]
        public async Task<ActionResult<InsuranceItem>> Get(Guid insuranceGuid, string customerNumber)
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
        /// <param name="item">InsuranceItem instance.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<InsuranceItem>> Post([FromBody] InsuranceItem item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (ValidateCustomerNumber(item.CustomerNumber))
                {
                    var storageItem = new StorageItem()
                    {
                        InsuranceGuid = item.InsuranceGuid,
                        CustomerNumber = item.CustomerNumber,
                        FirstName = item.FirstName,
                        LastName = item.LastName
                    };

                    var insurance = await _storageService.Add(storageItem);
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
        /// <summary>
        /// The method that updates the storage item in the database.
        /// </summary>
        /// <param name="item">InsuranceItem instance.</param>
        /// <param name="insuranceGuid">GuidID parameter.</param>
        /// <param name="customerNumber">Customer number parameter.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<InsuranceItem>> Put([FromBody] InsuranceItem item, Guid insuranceGuid, string customerNumber)
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
        public async Task<ActionResult<InsuranceItem>> Delete(Guid insuranceGuid, string customerNumber)
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
        /// <summary>
        /// The method that validates the customer number.
        /// </summary>
        /// <param name="customerNumber">Customer number parameter.</param>
        /// <returns></returns>
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
