using FluentValidation;
using Pony.Domain.Mazes.Commands;
using Pony.Domain.Mazes.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain.Mazes.Validators
{
    public class MoveMazeObjectsValidator : AbstractValidator<MoveMazeObjects>
    {
        private readonly IMazeRules _mazeRules;

        public MoveMazeObjectsValidator(IMazeRules mazeRules)
        {
            _mazeRules = mazeRules;

            RuleFor(m => m.Direction)
                .Must(HaveValidDirection).WithMessage("Possible directions east, west, north, south, stay");
        }

        private bool HaveValidDirection(string direction)
        {
            return _mazeRules.isValidDirection(direction);
        }
    }
}