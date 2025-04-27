using Ardalis.GuardClauses;
using AutoMapper;
using Ignite.MedicationRequest.API.Interfaces;
using Ignite.MedicationRequest.API.Models.ErrorHandling;
using Ignite.MedicationRequest.API.Services.Requests;

namespace Ignite.MedicationRequest.API.Services
{
    public class MedicationRequestService : IMedicationRequestService
    {
        private readonly IMedicationRequestRepository _medicationRequestRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly IClinicianRepository _clinicianRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public MedicationRequestService(IMedicationRequestRepository repository,
            IMedicationRepository medicationRepository,
            IClinicianRepository clinicianRepository,
            IPatientRepository patientRepository,
            IMapper mapper)
        {
            _medicationRequestRepository = repository;
            _mapper = mapper;
            _medicationRepository = medicationRepository;
            _clinicianRepository = clinicianRepository;
            _patientRepository = patientRepository;
        }

        public async Task<ErrorWrapper<Models.MedicationRequest>> CreateMedicationRequestAsync(CreateMedicationRequestRequest request)
        {
            Guard.Against.Null(request);
            Guard.Against.NegativeOrZero(request.PatientId);
            Guard.Against.NegativeOrZero(request.ClinicianId);
            Guard.Against.NegativeOrZero(request.MedicationId);
            //etc

            var clinician = await _clinicianRepository.GetByIdAsync(request.ClinicianId);
            if (clinician == null)
            {
                return new ErrorWrapper<Models.MedicationRequest>
                {
                    Status = Status.BadRequest,
                    ErrorMessage = "A clinician could not be found for the supplied ClinicianId."
                };
            }

            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
            {
                return new ErrorWrapper<Models.MedicationRequest>
                {
                    Status = Status.BadRequest,
                    ErrorMessage = "A patient could not be found for the supplied PatientId."
                };
            }

            var medication = await _medicationRepository.GetByIdAsync(request.MedicationId);
            if (medication == null)
            {
                return new ErrorWrapper<Models.MedicationRequest>
                {
                    Status = Status.BadRequest,
                    ErrorMessage = "A medication could not be found for the supplied MedicationId."
                };
            }

            var medicationRequest = _mapper.Map<Models.MedicationRequest>(request);
            medicationRequest.Clinician = clinician;
            medicationRequest.Patient = patient;
            medicationRequest.Medication = medication;

            await _medicationRequestRepository.CreateMedicationRequestAsync(medicationRequest);
            await _medicationRequestRepository.SaveChangesAsync();

            return new ErrorWrapper<Models.MedicationRequest>
            {
                Status = Status.Success,
                Data = medicationRequest
            };
        }

        public async Task<IEnumerable<Models.DTOs.GetMedicationRequestResultDto>> GetMedicationRequestsAsync(int patientId, DateTime? prescribedStartDate, DateTime? prescribedEndDate, Models.Enums.Status? status = null)
        {
            Guard.Against.NegativeOrZero(patientId);
            //etc

            var requests = await _medicationRequestRepository.GetMedicationRequestAsync(patientId, prescribedStartDate, prescribedEndDate, status);

            return requests;
        }
    }
}
