
/// <summary>
/// Проверка методов работы с сообщениями пользователя
/// </summary>
public class UserMessagesTests: TestingElement<IUserMessagesService>
{
    public override void OnTest()
    {
        AssertService<IUserMessagesService>(service =>
        {
            var signin = provider.Get<SigninUser>();
            signin.Signin("customer@gmail.com","customer@gmail.com");
            var before = service.GetInbox().Count();
            var userId = signin.Verify().Id;
            var message = new UserMessage()
            {
                FromUserId = userId,
                ToUserId = userId,
                Subject = "Новая тема",
                Text = "Тест"
            };
            service.Send(message, null);
            if ((service.GetInbox().Count() - before) != 1)
                return false;
            if (service.GetOutbox().Any(m => m.Id == message.Id) == false)
                return false;
            return true;
        },
        "Умеет получать, отправлять сообщения",
        "Не умеет получать, отправлять сообщения");
    }
}
