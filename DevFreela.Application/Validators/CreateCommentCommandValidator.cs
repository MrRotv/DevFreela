using DevFreela.Application.Commands.CreateComment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(p => p.Content)
                .MaximumLength(255)
                .WithMessage("A quantiade de caracteres do comentário não deve ultrapassar 255 caracteres!");
            RuleFor(p => p.IdProject)
                .GreaterThan(0)
                .WithMessage("O comentário precisa estar atrelado a algum projeto!");
            RuleFor(p => p.IdUser)
                .GreaterThan(0)
                .WithMessage("O comentário precisa estar atrelado a algum usuário!");
        }
    }
}
