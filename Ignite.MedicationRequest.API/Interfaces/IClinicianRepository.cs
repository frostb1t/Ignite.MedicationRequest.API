using Ignite.MedicationRequest.API.Models;

namespace Ignite.MedicationRequest.API.Interfaces
{
    public interface IClinicianRepository
    {
        public Task<Clinician?> GetByIdAsync(int id);
    }
}
