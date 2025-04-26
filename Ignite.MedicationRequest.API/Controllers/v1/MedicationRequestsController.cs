using AutoMapper;
using FluentValidation;
using Ignite.MedicationRequest.API.DTOs.Requests;
using Ignite.MedicationRequest.API.Extensions;
using Ignite.MedicationRequest.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ignite.MedicationRequest.API.Controllers.v1
{
    [ApiController]
    [Asp.Versioning.ApiVersion("1")]
    [Route("api/v{apiVersion:apiVersion}/patient/{patientId:int}/[controller]")]
    public class MedicationRequestsController : ControllerBase
    {
        private readonly ILogger<MedicationRequestsController> _logger;
        private readonly IMedicationRequestService _medicationRequestService;
        private readonly IMapper _mapper;

        public MedicationRequestsController(ILogger<MedicationRequestsController> logger,
            IMedicationRequestService medicationRequestService,
            IMapper mapper)
        {
            _logger = logger;
            _medicationRequestService = medicationRequestService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateMedicationRequest([FromRoute] int patientId, [FromBody] CreateMedicationRequestRequest request,
            [FromServices] IValidator<CreateMedicationRequestRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                ModelState.AddErrorsFromValidationResult(validationResult);
                return BadRequest(ModelState);
            }

            var createMedicationRequest = _mapper.Map<Services.Requests.CreateMedicationRequestRequest>(request);
            createMedicationRequest.PatientId = patientId;

            var response = await _medicationRequestService.CreateMedicationRequestAsync(createMedicationRequest);

            if (response.Status == Models.ErrorHandling.Status.BadRequest)
            {
                return BadRequest(response.ErrorMessage);
            }

            // Typically I'd prefer to return a 201 Created with the endpoint URI to access the created medication request (although this endpoint doesn't exist)
            return NoContent();
        }
    }
}
