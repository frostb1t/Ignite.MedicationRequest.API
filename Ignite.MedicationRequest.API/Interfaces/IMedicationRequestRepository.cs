using Ignite.MedicationRequest.API.Models.DTOs;
using Ignite.MedicationRequest.API.Models.Enums;

namespace Ignite.MedicationRequest.API.Interfaces
{
    public interface IMedicationRequestRepository
    {
        public Task<IEnumerable<GetMedicationRequestResultDto>> GetMedicationRequestAsync(int patientId, DateTime? prescribedStartDate, DateTime? prescribedEndDate, Status? status = null);
        public Task CreateMedicationRequestAsync(Models.MedicationRequest medicationRequest);
        public Task<int> SaveChangesAsync();
    }
}
