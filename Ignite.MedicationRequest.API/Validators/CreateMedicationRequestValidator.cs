using FluentValidation;
using Ignite.MedicationRequest.API.DTOs.Requests;

namespace Ignite.MedicationRequest.API.Validators
{
    public class CreateMedicationRequestValidator : AbstractValidator<CreateMedicationRequestRequest>
    {
        public CreateMedicationRequestValidator()
        {
            RuleFor(x => x.ClinicianId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.MedicationId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Reason)
                .NotEmpty()
                .MaximumLength(1024); // (An abritary value as an example)

            RuleFor(x => x.PrescribedDate)
                .Must(x => x <= DateTime.UtcNow)
                .NotEmpty()
                .WithMessage("The medication request prescribed date must be provided and be in the past.");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("The medication request start date must be provided.");

            When(x => x.EndDate.HasValue, () =>
            {
                RuleFor(x => x.EndDate)
                    .GreaterThan(x => DateTime.UtcNow)
                    .WithMessage("The medication request should end in the future.");

                RuleFor(x => x.EndDate)
                    .GreaterThan(x => x.StartDate)
                    .WithMessage("The medication request should end after it starts.");
            });

            RuleFor(x => x.Frequency)
                .NotEmpty()
                .MaximumLength(1024); // (An abritary value as an example)


            RuleFor(x => x.Status)
                .NotNull()
                .IsInEnum();
        }
    }
}
