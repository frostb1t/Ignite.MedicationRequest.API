using Ignite.MedicationRequest.API.Models;
using Ignite.MedicationRequest.API.Models.ErrorHandling;
using Ignite.MedicationRequest.API.Services.Requests;

namespace Ignite.MedicationRequest.API.Interfaces
{
    public interface IMedicationRequestService
    {
        public Task<ErrorWrapper<Models.MedicationRequest>> CreateMedicationRequest(CreateMedicationRequestRequest request);
    }
}
