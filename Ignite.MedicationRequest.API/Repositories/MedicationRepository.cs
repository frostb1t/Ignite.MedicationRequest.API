using Ignite.MedicationRequest.API.Data;
using Ignite.MedicationRequest.API.Interfaces;

namespace Ignite.MedicationRequest.API.Repositories
{
    public class MedicationRepository : IMedicationRepository
    {
        private ApplicationDbContext _context;
        public MedicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Models.Medication?> GetByIdAsync(int id)
        {
            return await _context.Medications.FindAsync(id);
        }
    }
}
