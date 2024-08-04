public interface IModalReference<T>
{
    public Task<T> Result { get; set; }

    public T Data { get; set; }
}