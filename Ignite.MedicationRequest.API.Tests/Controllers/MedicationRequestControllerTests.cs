using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Ignite.MedicationRequest.API.Controllers.v1;
using Ignite.MedicationRequest.API.DTOs.Requests;
using Ignite.MedicationRequest.API.Interfaces;
using Ignite.MedicationRequest.API.Mappings;
using Ignite.MedicationRequest.API.Models.ErrorHandling;
using Ignite.MedicationRequest.API.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Ignite.MedicationRequest.API.Tests.Controllers
{
    public class MedicationRequestControllerTests
    {
        private readonly Mock<IMedicationRequestService> _medicationRequestServiceMock = new();
        private readonly MedicationRequestsController _sut;

        public MedicationRequestControllerTests()
        {
            var mapper = new Mapper(new MapperConfiguration(opt => opt.AddProfile(typeof(MappingProfile))));
            _sut = new MedicationRequestsController(_medicationRequestServiceMock.Object, mapper);
        }

        [Fact]
        public async Task CreateMedicationRequest_ShouldReturn204NoContent_WhenRequestIsSuccessful()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = 1,
                MedicationId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(3),
                PrescribedDate = DateTime.UtcNow,
                Reason = "Example reason",
                Frequency = "Daily"
            };

            var validator = new CreateMedicationRequestValidator();

            var mockServiceResponse = new Mock<ErrorWrapper<Models.MedicationRequest>>();

            _medicationRequestServiceMock.Setup(svc => svc.CreateMedicationRequestAsync(It.IsAny<API.Services.Requests.CreateMedicationRequestRequest>()))
                .Returns(Task.FromResult(mockServiceResponse.Object));

            // Act
            var response = await _sut.CreateMedicationRequest(1, request, validator);

            // Assert
            var noContentResult = response as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task CreateMedicationRequest_ShouldReturnBadRequest_WhenRequestIsInvalid()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = 1,
                MedicationId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(3),
                PrescribedDate = DateTime.UtcNow,
            };

            var validatorMock = new Mock<IValidator<CreateMedicationRequestRequest>>();
            var invalidValidationResult = new Mock<ValidationResult>();
            invalidValidationResult.Setup(r => r.IsValid).Returns(false);

            validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<CreateMedicationRequestRequest>(), CancellationToken.None))
                .Returns(Task.FromResult(invalidValidationResult.Object));

            // Act
            var response = await _sut.CreateMedicationRequest(1, request, validatorMock.Object);

            // Assert
            var badRequestObjectResult = response as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();
            badRequestObjectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CreateMedicationRequest_ShouldReturnBadRequest_WhenServiceReturnsBadRequestStatus()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = 1,
                MedicationId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(3),
                PrescribedDate = DateTime.UtcNow,
                Reason = "Example",
                Frequency = "Daily"
            };

            var validator = new CreateMedicationRequestValidator();

            var mockServiceResponse = new ErrorWrapper<Models.MedicationRequest>();
            mockServiceResponse.Status = Status.BadRequest;

            var expectedErrorMessage = "Example error message";
            mockServiceResponse.ErrorMessage = expectedErrorMessage;

            _medicationRequestServiceMock.Setup(svc => svc.CreateMedicationRequestAsync(It.IsAny<API.Services.Requests.CreateMedicationRequestRequest>()))
                .Returns(Task.FromResult(mockServiceResponse));

            // Act
            var response = await _sut.CreateMedicationRequest(1, request, validator);

            // Assert
            var badRequestObjectResult = response as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();
            badRequestObjectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestObjectResult.Value.Should().Be(expectedErrorMessage);
        }
    }
}
