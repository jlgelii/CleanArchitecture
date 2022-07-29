using CleanArchitecture.Application.Configurations.Validation;
using CleanArchitecture.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Persons.Command.UpdatePerson
{
    public class UpdatePersonCommandValidator : IValidationHandler<UpdatePersonCommand>
    {
        public async Task<ValidationResults> Validate(UpdatePersonCommand request)
        {
            if (string.IsNullOrEmpty(request.Firstname))
                return ValidationResults.Fail("Firstname does not exist.");

            if (string.IsNullOrEmpty(request.Lastname))
                return ValidationResults.Fail("Lastname does not exist.");

            return ValidationResults.Success;
        }
    }
}
