using Ignite.MedicationRequest.API.Models.ErrorHandling;
using Ignite.MedicationRequest.API.Services.Requests;

namespace Ignite.MedicationRequest.API.Interfaces
{
    public interface IMedicationRequestService
    {
        public Task<ErrorWrapper<Models.MedicationRequest>> CreateMedicationRequestAsync(CreateMedicationRequestRequest request);
        public Task<IEnumerable<Models.DTOs.GetMedicationRequestResultDto>> GetMedicationRequestsAsync(int patientId, DateTime? prescribedStartDate, DateTime? prescribedEndDate, Models.Enums.Status? status = null);
    }
}
