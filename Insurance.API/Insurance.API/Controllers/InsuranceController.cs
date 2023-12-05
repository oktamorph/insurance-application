﻿using Insurance.API.Services;
using Insurance.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Insurance.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _service;
        public InsuranceController(IInsuranceService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var insurances = _service.GetInsuranceItems();

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
                var insurance = _service.GetById(id);

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

                var insurance = _service.Add(item);
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
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var insuranceUpdate = _service.GetById(id);

                if (insuranceUpdate == null)
                    return NotFound($"Insurance with Id = '{id} not found.");

                insuranceUpdate.FirstName = item.FirstName;
                insuranceUpdate.LastName = item.LastName;

                _service.Update(insuranceUpdate);
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
                var insurace = _service.GetById(id);

                if (insurace == null)
                    return NotFound($"Insurance with Id = '{id}' not found.");

                _service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
