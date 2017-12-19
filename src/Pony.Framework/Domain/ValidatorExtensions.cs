using FluentValidation;
using FluentValidation.Results;
using Pony.Framework.Commands;
using Pony.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Framework.Domain
{
    public static class ValidatorExtensions
    {
        public static void ValidateCommand<TCommand>(this IValidator<TCommand> validator, TCommand command) where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
                throw new ApiException(BuildErrorMesage(validationResult.Errors), 400);
        }

        private static string BuildErrorMesage(IEnumerable<ValidationFailure> errors)
        {
            var errorsText = errors.Select(x => " \n - " + x.ErrorMessage).ToArray();
            return "Validation failed: " + string.Join("", errorsText);
        }
    }
}