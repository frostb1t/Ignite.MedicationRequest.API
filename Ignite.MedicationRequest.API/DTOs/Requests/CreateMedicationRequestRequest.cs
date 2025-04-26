using Ignite.MedicationRequest.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ignite.MedicationRequest.API.DTOs.Requests
{
    public class CreateMedicationRequestRequest
    {
        [Required]
        public int ClinicianId { get; set; }

        [Required]
        public int MedicationId { get; set; }

        [Required]
        public string Reason { get; set; }

        /// <summary>
        /// The date the medication was prescribed (UTC Time)
        /// </summary>
        [Required]
        public DateTime PrescribedDate { get; set; }

        /// <summary>
        /// The date the medication should start (UTC Time)
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The date the medication should end (UTC Time)
        /// </summary>
        public DateTime? EndDate { get; set; }

        [Required]
        public string Frequency { get; set; }

        [Required]
        public Status Status { get; set; }
    }
}
