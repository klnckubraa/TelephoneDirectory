using FluentValidation;

namespace WebApplication1.DTOs.Validations
{
    public class DetailDtoValidation : AbstractValidator<DetailDto>
    {
        public DetailDtoValidation()
        {
            RuleFor(x => x.PersonId).NotEmpty().WithMessage("PersonId girmelisiniz.");
            RuleFor(x => x.TypeId).NotEmpty().WithMessage("TypeId girmelisiniz.");
            RuleFor(x => x.Number).Length(0, 10).WithMessage("Başında 0 olmadan numarayı tekrar giriniz.");
        }
    }
}
