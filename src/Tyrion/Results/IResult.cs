namespace Tyrion.Results
{
    public interface IResult
    {
        public bool Success { get; }

        public string Message { get; }
    }

    public interface IResult<out T>
    {
        public bool Success { get; }

        public string Message { get; }

        public T Data { get; }
    }
}
