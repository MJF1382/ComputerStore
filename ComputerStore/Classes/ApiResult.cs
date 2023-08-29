using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Classes
{
    public class ApiResult : IActionResult
    {
        public int StatusCode { get; private set; }
        public bool IsSuccess
        {
            get
            {
                string firstDiggit = StatusCode.ToString()[0].ToString();

                if (firstDiggit == "4" || firstDiggit == "5")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public object? Content { get; private set; }
        public List<string> Errors { get; private set; }

        public ApiResult(
            Status statusCode)
        {
            StatusCode = (int)statusCode;
            Content = null;
            Errors = new List<string>();
        }

        public ApiResult(
            Status statusCode,
            object? content)
        {
            StatusCode = (int)statusCode;
            Content = content;
            Errors = new List<string>();
        }

        public ApiResult(
            Status statusCode,
            object? content,
            List<string> errors)
        {
            StatusCode = (int)statusCode;
            Content = content;
            Errors = errors;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult result = new ObjectResult(this)
            {
                StatusCode = StatusCode
            };

            await result.ExecuteResultAsync(context);
        }
    }
}
