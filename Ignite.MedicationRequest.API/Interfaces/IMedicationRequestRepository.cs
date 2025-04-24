using Ignite.MedicationRequest.API.Models;

namespace Ignite.MedicationRequest.API.Interfaces
{
    public interface IMedicationRequestRepository
    {
        public Task CreateMedicationRequestAsync(Models.MedicationRequest medicationRequest);
        public Task<int> SaveChangesAsync();
    }
}
