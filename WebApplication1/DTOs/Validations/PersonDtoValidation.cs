using FluentValidation;

namespace WebApplication1.DTOs.Validations
{
    public class PersonDtoValidation : AbstractValidator<PersonDto>
    {

        public PersonDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim giriniz");

        }
    }
}
