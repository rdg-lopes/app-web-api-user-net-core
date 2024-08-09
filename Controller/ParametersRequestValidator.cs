using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserWebApi.Controllers;

public class ParametersRequestValidator : ActionFilterAttribute 
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid){
            var erros = new ErrosModelViewInput(new List<String>(context.ModelState.SelectMany(m => m.Value.Errors)
                                                                                 .Select(m => m.ErrorMessage)));
            context.Result = new BadRequestObjectResult(erros);
        }
    }
}