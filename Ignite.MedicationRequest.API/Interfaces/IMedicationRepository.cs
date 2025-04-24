namespace Ignite.MedicationRequest.API.Interfaces
{
    public interface IMedicationRepository
    {
        public Task<Models.Medication?> GetByIdAsync(int id);
    }
}
