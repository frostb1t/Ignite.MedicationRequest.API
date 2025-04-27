using Ignite.MedicationRequest.API.Data;
using Ignite.MedicationRequest.API.Interfaces;
using Ignite.MedicationRequest.API.Models.DTOs;
using Ignite.MedicationRequest.API.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Ignite.MedicationRequest.API.Repositories
{
    public class MedicationRequestRepository : IMedicationRequestRepository
    {
        private readonly ApplicationDbContext _context;
        public MedicationRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetMedicationRequestResultDto>> GetMedicationRequestAsync(int patientId, DateTime? prescribedStartDate, DateTime? prescribedEndDate, Status? status = null)
        {
            var medicationRequests = _context.MedicationRequests
                .AsNoTracking()
                .Include(mr => mr.Clinician)
                .Include(mr => mr.Medication)
                .Where(mr =>
                     mr.PatientId == patientId
                 );

            if (status.HasValue)
            {
                medicationRequests = medicationRequests.Where(mr => mr.Status == status.Value);
            }

            if (prescribedStartDate.HasValue)
            {
                medicationRequests = medicationRequests.Where(mr => mr.PrescribedDate >= prescribedStartDate.Value);
            }

            if (prescribedEndDate.HasValue)
            {
                medicationRequests = medicationRequests.Where(mr => mr.PrescribedDate <= prescribedEndDate.Value);
            }

            return await medicationRequests
                .Select(mr => new GetMedicationRequestResultDto
                {
                    ClinicianFirstName = mr.Clinician.FirstName,
                    ClinicianLastName = mr.Clinician.LastName,
                    MedicationCodeName = mr.Medication.CodeName
                })
                .ToListAsync();
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
