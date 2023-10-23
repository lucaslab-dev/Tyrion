namespace Tyrion
{
    public interface IResult
    {
        public bool Successed { get; }
        public bool Failed { get; }
        public string Message { get; }
    }

    public interface IResult<out T> : IResult
    {
        public T Data { get; }
    }
}
