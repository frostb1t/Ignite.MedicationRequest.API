using Ignite.MedicationRequest.API.Data;
using Ignite.MedicationRequest.API.Interfaces;

namespace Ignite.MedicationRequest.API.Repositories
{
    public class MedicationRequestRepository : IMedicationRequestRepository
    {
        private ApplicationDbContext _context;
        public MedicationRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateMedicationRequestAsync(Models.MedicationRequest medicationRequest)
        {
            await _context.MedicationRequests.AddAsync(medicationRequest);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
