namespace Entity.Result
{
    public class ResultBase
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
    }
}
