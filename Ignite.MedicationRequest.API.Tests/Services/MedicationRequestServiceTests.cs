using AutoMapper;
using FluentAssertions;
using Ignite.MedicationRequest.API.Interfaces;
using Ignite.MedicationRequest.API.Mappings;
using Ignite.MedicationRequest.API.Models;
using Ignite.MedicationRequest.API.Models.ErrorHandling;
using Ignite.MedicationRequest.API.Services;
using Ignite.MedicationRequest.API.Services.Requests;
using Moq;

namespace Ignite.MedicationRequest.API.Tests.Services
{
    public class MedicationRequestServiceTests
    {
        private readonly Mock<IMedicationRequestRepository> _medicationRequestRepository = new();
        private readonly Mock<IMedicationRepository> _medicationRepository = new();
        private readonly Mock<IClinicianRepository> _clinicianRepository = new();
        private readonly Mock<IPatientRepository> _patientRepository = new();
        private readonly IMedicationRequestService _sut;
        public MedicationRequestServiceTests()
        {
            var mapper = new Mapper(new MapperConfiguration(opt => opt.AddProfile(typeof(MappingProfile))));

            _sut = new MedicationRequestService(_medicationRequestRepository.Object,
                    _medicationRepository.Object,
                    _clinicianRepository.Object,
                    _patientRepository.Object,
                    mapper
                );
        }

        #region CreateMedicationRequest

        [Fact]
        public async void CreateMedicationRequest_ShouldReturnCorrectResponse_WhenRequestIsSuccessful()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = 1,
                PatientId = 1,
                MedicationId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                PrescribedDate = DateTime.UtcNow,
            };

            var mockClinician = new Mock<Clinician>().Object;
            _clinicianRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(mockClinician)!);

            var mockPatient = new Mock<Patient>().Object;
            _patientRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(mockPatient)!);

            var mockMedication = new Mock<Medication>().Object;
            _medicationRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(mockMedication)!);

            // Act
            var response = await _sut.CreateMedicationRequestAsync(request);

            //Assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().BeNullOrEmpty();
            response.Status.Should().Be(Status.Success);
            response.Data.Should().NotBeNull();
            response.Data.Medication.Should().BeEquivalentTo(mockMedication);
            response.Data.Clinician.Should().BeEquivalentTo(mockClinician);
            response.Data.Patient.Should().BeEquivalentTo(mockPatient);
        }

        [Fact]
        public async void CreateMedicationRequest_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            // Arrange
            CreateMedicationRequestRequest request = null;

            // Act
            var response = async () => await _sut.CreateMedicationRequestAsync(request!);

            //Assert
            await response.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async void CreateMedicationRequest_ShouldThrowArgumentException_WhenPatientIdIsZero()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = 1,
                PatientId = 0,
                MedicationId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                PrescribedDate = DateTime.UtcNow,
            };

            // Act
            var response = async () => await _sut.CreateMedicationRequestAsync(request);

            //Assert
            await response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async void CreateMedicationRequest_ShouldThrowArgumentException_WhenPatientIdIsNegative()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = 1,
                PatientId = -1,
                MedicationId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                PrescribedDate = DateTime.UtcNow,
            };

            // Act
            var response = async () => await _sut.CreateMedicationRequestAsync(request);

            //Assert
            await response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async void CreateMedicationRequest_ShouldThrowArgumentException_WhenClinicianIdIsZero()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = 0,
                PatientId = 1,
                MedicationId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                PrescribedDate = DateTime.UtcNow,
            };

            // Act
            var response = async () => await _sut.CreateMedicationRequestAsync(request);

            //Assert
            await response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async void CreateMedicationRequest_ShouldThrowArgumentException_WhenClinicianIdIsNegative()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = -1,
                PatientId = 1,
                MedicationId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                PrescribedDate = DateTime.UtcNow,
            };

            // Act
            var response = async () => await _sut.CreateMedicationRequestAsync(request);

            //Assert
            await response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async void CreateMedicationRequest_ShouldThrowArgumentException_WhenMedicationIdIsZero()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = 1,
                PatientId = 1,
                MedicationId = 0,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                PrescribedDate = DateTime.UtcNow,
            };

            // Act
            var response = async () => await _sut.CreateMedicationRequestAsync(request);

            //Assert
            await response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async void CreateMedicationRequest_ShouldThrowArgumentException_WhenMedicationIdIsNegative()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = 1,
                PatientId = 1,
                MedicationId = -1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                PrescribedDate = DateTime.UtcNow,
            };

            // Act
            var response = async () => await _sut.CreateMedicationRequestAsync(request);

            //Assert
            await response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async void CreateMedicationRequest_ShouldReturnBadRequestResponse_WhenNoClinicianIsFound()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = 1,
                PatientId = 1,
                MedicationId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                PrescribedDate = DateTime.UtcNow,
            };

            Clinician? returnedClinician = null;
            _clinicianRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(returnedClinician));

            // Act
            var response = await _sut.CreateMedicationRequestAsync(request);

            //Assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().NotBeNullOrEmpty();
            response.Status.Should().Be(Status.BadRequest);
            response.Data.Should().BeNull();
        }

        [Fact]
        public async void CreateMedicationRequest_ShouldReturnBadRequestResponse_WhenNoPatientIsFound()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = 1,
                PatientId = 1,
                MedicationId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                PrescribedDate = DateTime.UtcNow,
            };

            var mockClinician = new Mock<Clinician>().Object;
            _clinicianRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(mockClinician)!);

            Patient? returnedPatient = null;
            _patientRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(returnedPatient));

            // Act
            var response = await _sut.CreateMedicationRequestAsync(request);

            //Assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().NotBeNullOrEmpty();
            response.Status.Should().Be(Status.BadRequest);
            response.Data.Should().BeNull();
        }

        [Fact]
        public async void CreateMedicationRequest_ShouldReturnBadRequestResponse_WhenNoMedicationIsFound()
        {
            // Arrange
            var request = new CreateMedicationRequestRequest
            {
                ClinicianId = 1,
                PatientId = 1,
                MedicationId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                PrescribedDate = DateTime.UtcNow,
            };

            var mockClinician = new Mock<Clinician>().Object;
            _clinicianRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(mockClinician)!);

            var mockPatient = new Mock<Patient>().Object;
            _patientRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(mockPatient)!);

            Medication? returnedMedication = null;
            _medicationRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(returnedMedication));

            // Act
            var response = await _sut.CreateMedicationRequestAsync(request);

            //Assert
            response.Should().NotBeNull();
            response.ErrorMessage.Should().NotBeNullOrEmpty();
            response.Status.Should().Be(Status.BadRequest);
            response.Data.Should().BeNull();
        }
        #endregion

        #region GetMedicationRequests

        [Fact]
        public async void GetMedicationRequests_ShouldReturnCorrectResponse_WhenRequestIsSuccessful()
        {
            // Arrange

            // Act
            var response = await _sut.GetMedicationRequestsAsync(
                    1,
                    It.IsAny<DateTime?>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<Models.Enums.Status>()
                );

            var mockResults = new Mock<IEnumerable<Models.DTOs.GetMedicationRequestResultDto>>().Object;
            _medicationRequestRepository.Setup(r => r.GetMedicationRequestAsync(
                    It.IsAny<int>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<Models.Enums.Status>()
                ))
                .Returns(Task.FromResult(mockResults)!);

            //Assert
            response.Should().NotBeNull();
        }

        [Fact]
        public async void GetMedicationRequests_ShouldThrowArgumentException_WhenPatientIdIsZero()
        {
            // Arrange
            int patientId = 0;

            // Act
            var response = async () => await _sut.GetMedicationRequestsAsync(
                   patientId,
                   It.IsAny<DateTime?>(),
                   It.IsAny<DateTime?>(),
                   It.IsAny<Models.Enums.Status>()
               );

            //Assert
            await response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async void GetMedicationRequests_ShouldThrowArgumentException_WhenPatientIdIsNegative()
        {
            // Arrange
            int patientId = -1;

            // Act
            var response = async () => await _sut.GetMedicationRequestsAsync(
                   patientId,
                   It.IsAny<DateTime?>(),
                   It.IsAny<DateTime?>(),
                   It.IsAny<Models.Enums.Status>()
               );

            //Assert
            await response.Should().ThrowAsync<ArgumentException>();
        }

        #endregion

    }
}