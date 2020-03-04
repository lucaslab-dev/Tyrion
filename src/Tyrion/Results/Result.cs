namespace Tyrion.Results
{
    public sealed class Result : IResult
    {
        private Result() => Success = true;

        private Result(string message, bool success = false)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; }
        public string Message { get; }

        public static IResult Successed() => new Result();
        public static IResult Successed(string message) => new Result(message, true);
        public static IResult Failed(string message) => new Result(message);
    }

    public sealed class Result<T> : IResult<T>
    {
        private Result() => Success = true;

        private Result(T data)
        {
            Success = true;
            Data = data;
        }

        private Result(string message)
        {
            Success = false;
            Message = message;
        }

        public bool Success { get; }
        public string Message { get; }
        public T Data { get; }

        public static IResult<T> Successed() => new Result<T>();
        public static IResult<T> Successed(T data) => new Result<T>(data);
        public static IResult<T> Failed(string message) => new Result<T>(message);
    }
}
