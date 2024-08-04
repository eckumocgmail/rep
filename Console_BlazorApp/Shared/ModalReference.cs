public class ModalReference<T>: IModalReference<T>
{
    public Task<T> Result { get; set; }
    public T Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}