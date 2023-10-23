using System.Threading.Tasks;

namespace Tyrion
{
    public class Result : IResult
    {
        protected Result() => Successed = true;

        protected Result(string message, bool successed)
        {
            Successed = successed;
            Failed = !successed;
            Message = message;
        }

        public bool Successed { get; protected set; }

        public bool Failed { get; protected set; }

        public string Message { get; protected set; }

        public static IResult Success() => new Result();

        public static IResult Success(string message) => new Result(message, true);

        public static Task<IResult> SuccessAsync() => Task.FromResult(Success());

        public static Task<IResult> SuccessAsync(string message) => Task.FromResult(Success(message));

        public static IResult Fail(string message) => new Result(message, false);

        public static Task<IResult> FailAsync(string message) => Task.FromResult(Fail(message));
    }

    public sealed class Result<T> : Result, IResult<T>
    {
        private Result() => Successed = true;

        private Result(T data, bool successed)
        {
            Successed = successed;
            Failed = !successed;
            Data = data;
        }

        private Result(string message, bool successed)
        {
            Successed = successed;
            Failed = !successed;
            Message = message;
        }

        public T Data { get; }

        public new static IResult<T> Success() => new Result<T>();

        public static IResult<T> Success(T data) => new Result<T>(data, true);

        public static Task<IResult<T>> SuccessAsync(T data) => Task.FromResult(Success(data));

        public new static IResult<T> Fail(string message) => new Result<T>(message, false);

        public new static Task<IResult<T>> FailAsync(string message) => Task.FromResult(Fail(message));
    }
}
