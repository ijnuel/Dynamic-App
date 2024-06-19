namespace Application.Models
{
    public class Result<T>
    {
        public Result(bool succeeded, string message, T? result, string exception = null)
        {
            Succeeded = succeeded;
            Message = message;
            ExceptionError = exception;
            Entity = result;
        }
        public Result(bool succeeded, string message, string exception = null)
        {
            Succeeded = succeeded;
            Message = message;
            ExceptionError = exception;
        }

        public Result(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public Result(bool succeeded, T? result)
        {
            Succeeded = succeeded;
            Entity = result;
        }

        public bool Succeeded { get; set; }

        public T? Entity { get; set; }
        public string ExceptionError { get; set; }

        public string Message { get; set; }

        public static Result<T> Success()
        {
            return new Result<T>(true);
        }

        public static Result<string> Success(string message)
        {
            return new Result<string>(true, message);
        }

        public static Result<string> Success(Guid message)
        {
            return new Result<string>(true, message.ToString());
        }

        public static Result<T> Success(string message, T entity)
        {
            return new Result<T>(true, message, entity);
        }

        public static Result<T> Success(T entity)
        {
            return new Result<T>(true, entity);
        }

        public static Result<T> Failure()
        {
            return new Result<T>(false);
        }

        public static Result<T> Failure(string message, T entity)
        {
            return new Result<T>(false, message, entity);
        }
        public static Result<string> Failure(string error)
        {
            return new Result<string>(false, error);
        }

        public static Result<string> Failure(string prefixMessage, Exception ex)
        {
            return new Result<string>(false, $"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
        }

        public static Result<T> Failure(T entity)
        {
            return new Result<T>(false, entity);
        }

        public static Result<string> Info(string error)
        {
            return new Result<string>(true, error);
        }

        public static Result<T> Info(T entity)
        {
            return new Result<T>(true, entity);
        }

        public static Result<string> Warning(string error)
        {
            return new Result<string>(false, error);
        }

        public static Result<T> Warning(T entity)
        {
            return new Result<T>(false, entity);
        }

        public static Result<T> Exception(Exception ex)
        {
            return new Result<T>(false, ex.Message, ex?.InnerException?.Message);
        }
    }
}
