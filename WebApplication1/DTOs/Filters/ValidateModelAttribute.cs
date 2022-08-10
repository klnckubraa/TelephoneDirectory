using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.DTOs.Validations;
using WebApplication1.Repositories;

namespace WebApplication1.DTOs.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                base.OnActionExecuting(context);
                context.Result = new BadRequestObjectResult(ResponseModal<DetailDtoValidation>.Error(errors, "Tekrar deneyiniz", false));
            }
        }
    }
}
