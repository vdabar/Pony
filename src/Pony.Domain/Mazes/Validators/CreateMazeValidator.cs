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
    public class CreateMazeValidator : AbstractValidator<CreateMaze>
    {
        private readonly IMazeRules _mazeRules;

        public CreateMazeValidator(IMazeRules mazeRules)
        {
            _mazeRules = mazeRules;
            RuleFor(m => m.MazeHeight)
                .GreaterThan(14).WithMessage("Maze dimensions should be between 15 and 25")
                .LessThan(26).WithMessage("Maze dimensions should be between 15 and 25");
            RuleFor(m => m.MazeWidth)
                .GreaterThan(14).WithMessage("Maze dimensions should be between 15 and 25")
                .LessThan(26).WithMessage("Maze dimensions should be between 15 and 25");
            RuleFor(m => m.MazePlayerName)
                .Must(HaveValidPonyNameAsync).WithMessage("Only ponies can play");
            RuleFor(m => m.Difficulty)
                .GreaterThanOrEqualTo(0).WithMessage("Difficulty should be between 0 and 10")
                .LessThanOrEqualTo(10).WithMessage("Difficulty should be between 0 and 10");
        }

        private bool HaveValidPonyNameAsync(string name)
        {
            return _mazeRules.IsPonyNameValidAsync(name);
        }
    }
}