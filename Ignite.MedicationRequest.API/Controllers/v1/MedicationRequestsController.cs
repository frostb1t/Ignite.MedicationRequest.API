using Microsoft.AspNetCore.Mvc;

namespace Ignite.MedicationRequest.API.Controllers.v1
{
    [ApiController]
    [Asp.Versioning.ApiVersion("1")]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class MedicationRequestsController : ControllerBase
    {
        private readonly ILogger<MedicationRequestsController> _logger;

        public MedicationRequestsController(ILogger<MedicationRequestsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }
    }
}
