using FluentValidation;

namespace WebApplication1.DTOs.Validations
{
    public class PersonCreateDtoValidation : AbstractValidator<PersonCreateDto>
    {

        public PersonCreateDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim giriniz");

        }
    }
}
