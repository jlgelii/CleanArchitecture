using CleanArchitecture.Application.Configurations.Validation;
using CleanArchitecture.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Persons.Command.CreatePerson
{
    public class CreatePersonCommandValidator : IValidationHandler<CreatePersonCommand>
    {
        public async Task<ValidationResults> Validate(CreatePersonCommand request)
        {
            if (string.IsNullOrEmpty(request.Firstname))
                return ValidationResults.Fail("Please input firstname");

            if (string.IsNullOrEmpty(request.Lastname))
                return ValidationResults.Fail("Please input lastname");

            return ValidationResults.Success;
        }
    }
}
