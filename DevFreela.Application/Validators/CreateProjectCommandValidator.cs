using DevFreela.Application.Commands.CreateProject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(p => p.Description)
                .MaximumLength(255)
                .WithMessage("Quantidade máxima de caracteres ultrapassada!");
            RuleFor(p => p.Title)
                .MaximumLength(30)
                .MinimumLength(2)
                .WithMessage("O título não pode ter apenas um caractere e deve ter no máximo 30");
        }
    }
}
