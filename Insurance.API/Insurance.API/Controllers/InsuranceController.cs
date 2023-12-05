using Microsoft.AspNetCore.Mvc;

namespace Insurance.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _service;
    }
}
