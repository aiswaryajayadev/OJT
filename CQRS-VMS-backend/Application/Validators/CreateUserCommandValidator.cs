using Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
     
            public CreateUserCommandValidator()
            {
                RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
                RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                    .WithMessage("Invalid email format.");
            }
        }
    }

