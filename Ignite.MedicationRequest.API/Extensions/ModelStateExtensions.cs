using Microsoft.AspNetCore.Mvc.ModelBinding;
using FluentValidation.Results;

namespace Ignite.MedicationRequest.API.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddErrorsFromValidationResult(this ModelStateDictionary modelState, ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
