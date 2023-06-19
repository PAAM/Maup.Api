using FluentValidation;
using Maup.Core.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Infrastructure.Validations
{
    public class PropertyValidator : AbstractValidator<PropertyDto>
    {
        public PropertyValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name cannot be null");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address cannot be null");
            RuleFor(x => x.Price).NotNull().NotEmpty().WithMessage("Price cannot be null");
            RuleFor(x => x.CodeInternal).NotNull().NotEmpty().WithMessage("CodeInternal cannot be null");
            RuleFor(x => x.Year).NotNull().NotEmpty().WithMessage("Year cannot be null");
            RuleFor(x => x.IdOwner).NotNull().NotEmpty().WithMessage("IdOwner cannot be null");
        }
    }
}
