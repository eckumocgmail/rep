using Microsoft.EntityFrameworkCore;

namespace Console_BlazorApp.AppUnits.AuthorizationTests
{
    [Label("Тестирование обмена сообщениями между пользователями")]
    public class UserMessagesTest: TestingElement
    {
        public UserMessagesTest(IServiceProvider provider) : base(provider)
        {
        }

        public override void OnTest() 
        {
            var context = provider.Get<DbContextUser>();
            var signup = provider.Get<SignupUser>();
            if (context.UserContexts_.Count() == 0 )
            {
                var res1 = signup.Signup(new UserAccount()
                {
                    Email = "putin@gmail.com",
                    Password = "putin@gmail.com",
                }, new UserPerson()
                {
                    FirstName = "Владимир",
                    LastName = "Владимирович",
                    SurName = "Путин",
                    Birthday = DateTime.Parse("26.08.1989"),
                    Tel = "7-921-090-3533"
                });

            
                var res2 = signup.Signup(new UserAccount()
                {
                    Email = "putin2@gmail.com",
                    Password = "putin2@gmail.com",
                }, new UserPerson()
                {
                    FirstName = "Александр",
                    LastName = "Александрович",
                    SurName = "Батов",
                    Birthday = DateTime.Parse("26.08.1989"),
                    Tel = "7-921-090-3534"
                });
            } 
            context.Add(new UserMessage()
            {
                Subject = "Тест",
                Text = "Это тестовое сообщение",
                FromUserID = context.UserContexts_.First().Id,// context.UserContexts_.Include(u => u.Account).FirstOrDefault(user => user.Account.Email.ToLower() == "eckumoc@gmail.com").Id,
                ToUserID = context.UserContexts_.First().Id//context.UserContexts_.Include(u => u.Account).FirstOrDefault(user => user.Account.Email.ToLower() == "eckumocuk@gmail.com").Id
            });
            context.SaveChanges();
            Messages.Add("Регистрация отправки сообщения прошла успешно");


        }
    }
}
