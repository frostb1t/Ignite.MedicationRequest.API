using FluentValidation;
using Ignite.MedicationRequest.API.DTOs.Requests;

namespace Ignite.MedicationRequest.API.Validators
{
    public class GetMedicationRequestValidator : AbstractValidator<GetMedicationRequestsRequest>
    {
        public GetMedicationRequestValidator()
        {
            RuleFor(x => x.Status).IsInEnum();

            When(x => x.PrescribedEndDate.HasValue && x.PrescribedStartDate.HasValue, () =>
            {
                RuleFor(x => x.PrescribedEndDate)
                    .GreaterThan(x => x.PrescribedStartDate)
                    .WithMessage("The prescribed end date filter should be after the prescribed start date filter.");
            });
        }
    }
}
