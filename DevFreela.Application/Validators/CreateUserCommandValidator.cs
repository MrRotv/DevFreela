using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage("E-mail inválido");

            RuleFor(p => p.Password)
                .Must(ValidPassword)
                .WithMessage("Senha deve contar pelo menos uma letra maiúscula, uma minúscula, um número, um caractere especial e no mínimo 8 caracteres.");

            RuleFor(p => p.FullName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome precisa ser preenchido");
        }

        public bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

            return regex.IsMatch(password);
        }
    }
}
