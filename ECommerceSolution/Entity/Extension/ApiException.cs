namespace Entity.Extension
{
    public class ApiException : Exception
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }
        public object CustomData { get; }

        public ApiException(string message, string errorCode, int statusCode = 500, object? customData = null)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            CustomData = customData;
        }
    }
}
