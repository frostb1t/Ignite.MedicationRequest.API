using Microsoft.EntityFrameworkCore;
using Ignite.MedicationRequest.API.Models;

namespace Ignite.MedicationRequest.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Models.MedicationRequest> MedicationRequests { get; set; }
        public DbSet<Clinician> Clinicians { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medication> Medications { get; set; }
    }
}
