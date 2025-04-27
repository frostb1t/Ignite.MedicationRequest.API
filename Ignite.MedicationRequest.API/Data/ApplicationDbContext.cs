using Ignite.MedicationRequest.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ignite.MedicationRequest.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Models.MedicationRequest> MedicationRequests { get; set; }
        public DbSet<Clinician> Clinicians { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medication> Medications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresDb"))
                .UseSeeding((context, _) =>
                {
                    var exampleClinician = context.Set<Clinician>().FirstOrDefault(c => c.Id == 1);
                    if (exampleClinician == null)
                    {
                        context.Set<Clinician>().Add(new Clinician
                        {
                            Id = 1,
                            FirstName = "Dr",
                            LastName = "Dre",
                            RegistrationId = Guid.NewGuid()
                        });
                        context.SaveChanges();
                    }

                    var exampleClinician2 = context.Set<Clinician>().FirstOrDefault(c => c.Id == 2);
                    if (exampleClinician2 == null)
                    {
                        context.Set<Clinician>().Add(new Clinician
                        {
                            Id = 2,
                            FirstName = "Another",
                            LastName = "Clinican",
                            RegistrationId = Guid.NewGuid()
                        });
                        context.SaveChanges();
                    }

                    var examplePatient = context.Set<Patient>().FirstOrDefault(c => c.Id == 1);
                    if (examplePatient == null)
                    {
                        context.Set<Patient>().Add(new Patient
                        {
                            Id = 1,
                            FirstName = "Josh",
                            LastName = "Wright",
                            DateOfBirth = DateTime.UtcNow.AddYears(-20),
                            Sex = Models.Enums.Sex.Male
                        });
                        context.SaveChanges();
                    }

                    var examplePatient2 = context.Set<Patient>().FirstOrDefault(c => c.Id == 2);
                    if (examplePatient2 == null)
                    {
                        context.Set<Patient>().Add(new Patient
                        {
                            Id = 2,
                            FirstName = "Second",
                            LastName = "Patient",
                            DateOfBirth = DateTime.UtcNow.AddYears(-40),
                            Sex = Models.Enums.Sex.Female
                        });
                        context.SaveChanges();
                    }

                    var exampleMedication = context.Set<Medication>().FirstOrDefault(c => c.Id == 1);
                    if (exampleMedication == null)
                    {
                        context.Set<Medication>().Add(new Medication
                        {
                            Id = 1,
                            Code = "747006",
                            CodeName = "Oxamniquine",
                            CodeSystem = "SNOMED",
                            StrengthValue = 5,
                            StrengthUnit = "g/ml",
                            Form = Models.Enums.Form.Powder
                        });
                        context.SaveChanges();
                    }

                    var exampleMedication2 = context.Set<Medication>().FirstOrDefault(c => c.Id == 2);
                    if (exampleMedication2 == null)
                    {
                        context.Set<Medication>().Add(new Medication
                        {
                            Id = 2,
                            Code = "000111",
                            CodeName = "Example",
                            CodeSystem = "SNOMED",
                            StrengthValue = 1,
                            StrengthUnit = "g/ml",
                            Form = Models.Enums.Form.Syrup
                        });
                        context.SaveChanges();
                    }
                });
        }

    }
}
