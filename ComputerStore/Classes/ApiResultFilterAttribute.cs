using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Classes
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ApiResult == false)
            {
                ICollection<string[]>? errorsDictionary = ((ValidationProblemDetails)((ObjectResult)context.Result).Value)?.Errors.Values;

                List<string> errors = new List<string>();

                if (errorsDictionary != null)
                {
                    foreach (string[] propertyError in errorsDictionary)
                    {
                        foreach (string error in propertyError)
                        {
                            errors.Add(error);
                        }
                    }

                    context.Result = new ApiResult(Status.BadRequest, errors);
                }
                else
                {
                    context.Result = new ApiResult(Status.BadRequest, null);
                }
            }

            base.OnResultExecuting(context);
        }
    }
}
