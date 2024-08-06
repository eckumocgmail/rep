public interface IModalService
{
    public IModalReference<T> Show<T>(string v, ModalParameters parmeters, ModalOptions modalOptions);
}