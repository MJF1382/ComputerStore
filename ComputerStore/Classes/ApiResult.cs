namespace ComputerStore.Classes
{
    public class ApiResult
    {
        public int StatusCode { get; private set; }
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
    }
}
