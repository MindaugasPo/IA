using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ValidationService;
using Types;

namespace IA.Filters
{
    public class IaValidationFilter : IActionFilter
    {
        private readonly IAValidatorFactory _validatorFactory;

        public IaValidationFilter(IAValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var arguments = context.ActionArguments;
            foreach (var arg in arguments)
            {
                var validator = _validatorFactory.GetValidator(arg.Value);
                if (!validator.IsValid())
                {
                    context.Result = new JsonResult(new AjaxResult() { Success = false, Message = validator.Errors() });
                }
            }
        }
    }
}
