namespace Entity.Result
{
    public class Result<T> : ResultBase
    {
        public T? Data { get; set; }
        public static Result<T> SuccessResult(T data, string message = "Operation succesfully operated")
        {
            return new Result<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static Result<T> SuccessResult()
        {
            return new Result<T>
            {
                Success = true,
                Message = "Operation successfully operated",
                Data = default
            };
        }

        public static Result<T> ErrorResult(string message, string errorCode = "", T data = default!)
        {
            return new Result<T>
            {
                Success = false,
                Message = message,
                ErrorCode = errorCode,
                Data = data
            };
        }
        public static Result<T> ErrorResult()
        {
            return new Result<T>
            {
                Success = false,
                Message = "",
                ErrorCode = "",
                Data = default,
            };
        }
    }
}
