using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleAuth.Common;
using System;
using System.Linq;

namespace SimpleAuth.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("model") && context.ActionArguments.FirstOrDefault(arg => arg.Key.Equals("model")).Value == null)
            {
                var responseResult = new ResponseResult
                {
                    Success = false,
                    Messages = new[] { "Model cannot be null" }
                };
                context.Result = new JsonResult(responseResult);
                return;
            }
            if (context.ModelState.IsValid)
            {
                return;
            }
            var rr = CreateResponseResultFromModelErrors(context);
            context.Result = new JsonResult(rr);
        }

        private ResponseResult CreateResponseResultFromModelErrors(ActionExecutingContext actionContext)
        {
            var messages = actionContext.ModelState.SelectMany(state => state.Value.Errors.Select(x => !String.IsNullOrEmpty(x.ErrorMessage) ? x.ErrorMessage : x.Exception.Message));
            return new ResponseResult
            {
                Success = false,
                Messages = messages.ToArray()
            };
        }
    }
}
