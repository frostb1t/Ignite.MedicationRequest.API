using Ignite.MedicationRequest.API.Data;
using Ignite.MedicationRequest.API.Interfaces;
using Ignite.MedicationRequest.API.Models;

namespace Ignite.MedicationRequest.API.Repositories
{
    public class ClinicianRepository : IClinicianRepository
    {
        private ApplicationDbContext _context;
        public ClinicianRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Clinician?> GetByIdAsync(int id)
        {
            return await _context.Clinicians.FindAsync(id);
        }
    }
}
