using FluentValidation;
using System.Text.RegularExpressions;
using TestApplication.Entities.Dto;

namespace TestApplication.Validators
{
    public class PersonRequestModelValidator : AbstractValidator<PersonRequestModel>
    {
        public PersonRequestModelValidator()
        {
            RuleFor(x => x.Name).Length(0, 100).NotNull().NotEmpty().WithMessage("Name is required")
                 .Matches(new Regex(@"^[а-яА-я ]+$")).WithMessage("Name is not valid");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().WithMessage("Phone Number is required.")
                 .Length(16).WithMessage("PhoneNumber must be 16 length.")
                 .Matches(new Regex(@"^\+7\([0-9]{3}\)[0-9]{3} [0-9]{2} [0-9]{2}$")).WithMessage("PhoneNumber is not valid");
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty().WithMessage("Email is required"); ;
            RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("City is required")
                .MinimumLength(3).WithMessage("City must not be less than 3 characters.")
                .Matches(new Regex(@"^[а-яА-я ]+$")).WithMessage("City not valid");
        }
    }
}
