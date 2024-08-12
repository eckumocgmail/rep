using Console_BlazorApp.AppUnits.DeliveryModel;

public interface IUserMessagesService
{
    Dictionary<string, int> GetUsers();
    List<UserMessage> GetInbox();
    List<UserMessage> GetOutbox();
    Task<int> Send(string subject, string text, int fromUserIID, int toUserId, List<ProductImage> files);

    /// <summary>
    /// Кол-во непрочитанных сообщений
    /// </summary>    
    int GetInboxNotReaded();

}