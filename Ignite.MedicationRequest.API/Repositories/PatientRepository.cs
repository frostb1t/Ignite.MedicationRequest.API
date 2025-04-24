using Ignite.MedicationRequest.API.Data;
using Ignite.MedicationRequest.API.Interfaces;
using Ignite.MedicationRequest.API.Models;

namespace Ignite.MedicationRequest.API.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }
    }
}
