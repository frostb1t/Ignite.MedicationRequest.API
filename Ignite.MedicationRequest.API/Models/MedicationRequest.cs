using Ignite.MedicationRequest.API.Models.Enums;

namespace Ignite.MedicationRequest.API.Models
{
    public class MedicationRequest
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Clinician Clinician { get; set; }
        public Medication Medication { get; set; }
        public string Reason { get; set; }
        public required DateTime PrescribedDate { get; set; }
        public required DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Frequency { get; set; }
        public Status Status { get; set; }
    }
}
