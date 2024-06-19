namespace Application.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel(bool succeeded, string message, T? result, string exception = null)
        {
            Succeeded = succeeded;
            Message = message;
            ExceptionError = exception;
            Result = result;
        }
        public ResponseModel(bool succeeded, string message, string exception = null)
        {
            Succeeded = succeeded;
            Message = message;
            ExceptionError = exception;
        }

        public ResponseModel(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public ResponseModel(bool succeeded, T? result)
        {
            Succeeded = succeeded;
            Result = result;
        }

        public bool Succeeded { get; set; }

        public T? Result { get; set; }
        public string ExceptionError { get; set; }

        public string Message { get; set; }

        public static ResponseModel<T> Success()
        {
            return new ResponseModel<T>(true);
        }

        public static ResponseModel<string> Success(string message)
        {
            return new ResponseModel<string>(true, message);
        }

        public static ResponseModel<string> Success(Guid message)
        {
            return new ResponseModel<string>(true, message.ToString());
        }

        public static ResponseModel<T> Success(string message, T entity)
        {
            return new ResponseModel<T>(true, message, entity);
        }

        public static ResponseModel<T> Success(T entity)
        {
            return new ResponseModel<T>(true, entity);
        }

        public static ResponseModel<T> Failure()
        {
            return new ResponseModel<T>(false);
        }

        public static ResponseModel<T> Failure(string message, T entity)
        {
            return new ResponseModel<T>(false, message, entity);
        }
        public static ResponseModel<string> Failure(string error)
        {
            return new ResponseModel<string>(false, error);
        }

        public static ResponseModel<string> Failure(string prefixMessage, Exception ex)
        {
            return new ResponseModel<string>(false, $"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
        }

        public static ResponseModel<T> Failure(T entity)
        {
            return new ResponseModel<T>(false, entity);
        }

        public static ResponseModel<string> Info(string error)
        {
            return new ResponseModel<string>(true, error);
        }

        public static ResponseModel<T> Info(T entity)
        {
            return new ResponseModel<T>(true, entity);
        }

        public static ResponseModel<string> Warning(string error)
        {
            return new ResponseModel<string>(false, error);
        }

        public static ResponseModel<T> Warning(T entity)
        {
            return new ResponseModel<T>(false, entity);
        }

        public static ResponseModel<T> Exception(Exception ex)
        {
            return new ResponseModel<T>(false, ex.Message, ex?.InnerException?.Message);
        }
    }
}
