using Ignite.MedicationRequest.API.Models;

namespace Ignite.MedicationRequest.API.Interfaces
{
    public interface IPatientRepository
    {
        public Task<Patient?> GetByIdAsync(int id);
    }
}
