[Label("Тест: Коллекция активных объектов")]
[Description("Коллекция должна уметь выдавать уникальные токены, при добавлении объекта," +
    " причём необходимо исключить возможность повторно добавить объект. " +
    "По токену получать объект и отмечать время его действия. " +
    "Удалять объект из коллекции по токену. " +
    "Отчищать коллекцию от неактивных объектов по заданному интервалу.")]
public class AuthorizationCollectionTest : TestingElement
{    
    public AuthorizationCollectionTest(IServiceProvider provider): base(provider)
    {        
    }

    public override void OnTest()
    {
        try
        {
            AuthorizationCollection<UserContext> collection = provider.Get<AuthorizationUsers>();
            var item = new UserContext();
            var token = "";
            Assert((test) => 
            {                
                var token1 = collection.Put(item);
                var token2 = token = collection.Put(item);
                collection.Find(item);
                return collection.GetAll().Count(next => next == item) == 1;
            }, 
                "Выдача токенов работает корректно, в коллекции нет возможности добавить один и тот же объект",
                "Выдача токенов работает не корректно, в коллекции есть возможность добавлять один и тот же объект");

            Assert((test) => token == collection.Find(item), 
                "Получение токена по объекту работает корректно",
                "Получение токена по объекту работает корректно");

            Assert((test) => item == collection.Take(token),
                "Получение активного объекта по токену работает корректно",
                "Получение активного объекта по токену работает некорректно");

            Assert((test) => item == collection.Remove(token),
                "Удаление активного объекта по токену работает корректно",
                "Удаление активного объекта по токену работает некорректно");
            token = collection.Put(item);
            Thread.Sleep(1000);
            
            Assert((test) => collection.GetNotActive(999).Count() > 0,
                "Удаление неактивных объектов работает корректно",
                "Удаление неактивных объектов работает не корректно");
            Failed = false;
        }
        catch(Exception ex)
        {
            Failed = true;
            Messages.Add(ex.Message);
        }

    }
}
