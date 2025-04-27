namespace Ignite.MedicationRequest.API.DTOs.Responses
{
    public class GetMedicationRequestsResponse
    {
        public IEnumerable<GetMedicationRequestResult> Results { get; set; }
    }
}
