using Ignite.MedicationRequest.API.Models.Enums;

namespace Ignite.MedicationRequest.API.Services.Requests
{
    public class CreateMedicationRequestRequest
    {
        public int PatientId { get; set; }

        public int ClinicianId { get; set; }

        public int MedicationId { get; set; }

        public string Reason { get; set; }

        /// <summary>
        /// The date the medication was prescribed (UTC Time)
        /// </summary>
        public DateTime PrescribedDate { get; set; }

        /// <summary>
        /// The date the medication should start (UTC Time)
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The date the medication should end (UTC Time)
        /// </summary>
        public DateTime? EndDate { get; set; }

        public string Frequency { get; set; }

        public Status Status { get; set; }
    }
}
