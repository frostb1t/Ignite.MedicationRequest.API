using Ignite.MedicationRequest.API.Models.Enums;

namespace Ignite.MedicationRequest.API.DTOs.Requests
{
    public class GetMedicationRequestsRequest
    {
        public Status? Status { get; set; } = null;

        public DateTime? PrescribedStartDate { get; set; }

        public DateTime? PrescribedEndDate { get; set; }
    }
}
