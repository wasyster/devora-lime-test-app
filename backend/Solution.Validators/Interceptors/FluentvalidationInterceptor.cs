namespace Solution.Validators.Interceptors;

public class FluentvalidationInterceptor : IValidatorInterceptor
{
    public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
    {
        if (!result.IsValid)
        {
            ICollection<GlobalErrorResponse> validationErrors = new List<GlobalErrorResponse>();

            foreach (ValidationFailure error in result.Errors)
            {
                validationErrors.Add(new GlobalErrorResponse($"{error.PropertyName} : {error.ErrorMessage}"));
            }

            string errorMessages = JsonSerializer.Serialize(validationErrors);
            throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessages, "Validation failed!");
        }

        return result;
    }

    public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
    {
        return commonContext;
    }
}
