using Console_BlazorApp.AppUnits.DeliveryModel;

public interface IUserMessagesService
{
    Dictionary<string, int> GetUsers();
    List<UserMessage> GetInbox();
    List<UserMessage> GetOutbox();
    Task<int> Send(UserMessage p, List<ProductImage> files);


    /// <summary>
    /// Кол-во непрочитанных сообщений
    /// </summary>    
    int GetInboxNotReaded();

}