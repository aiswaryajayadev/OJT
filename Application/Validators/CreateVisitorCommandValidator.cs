using Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateVisitorCommandValidator : AbstractValidator<CreateVisitorCommand>
    {
        public CreateVisitorCommandValidator()
        {
            RuleFor(x => x.VisitorDto.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.VisitorDto.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
            RuleFor(x => x.VisitorDto.PurposeOfVisitId).GreaterThan(0).WithMessage("Purpose of visit is required.");
            RuleFor(x => x.VisitorDto.PersonInContact).NotEmpty().WithMessage("Person in contact is required.");
        }
    }


}
