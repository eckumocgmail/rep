using Newtonsoft.Json;

[Icon("message")]
[Label("Поле сообщения")]
public class MessageProperty: IObjectWithId
{
    [Label("Надпись")]
    [HelpMessage("Надпись располагается рядом над элементом ввода")]
    [NotNullNotEmpty("Введите ")]
    [InputRusText("Используйте русскую кирилицу для надписи поля ввода")]
    public string Label { get; set; } = "Свойство";


    [Label("Имя в наборе данных")]
    [HelpMessage("Имя свойства сообщения является идентификатором в наборе данных")]
    [NotNullNotEmpty("Введите имя свойства сообщения")]
    [InputEngText("Используйте латиницу для имени свойства сообщения")]    
    public string Name { get; set; } = "New property";


    [Label("Порядковый номер")]
    public int ZOrder { get; set; } = 0;


    [Label("Подпись")]
    [InputRusText("Используйте русскую кирилицу")]
    public string Help { get; set; } = "Подсказка";

    [Label("Обязательное")]
    public bool Required { get; set; } = false;

    [Label("Уникальное")]
    public bool Unique { get; set; } = false;

    [Label("Создание индекса")]
    [HelpMessage("Индексируемые поля являются ключами для поиска")]
    public bool Index { get; set; } = false;



    [Label("Атрибут")]
    [SingleSelectApi(nameof(MessageAttribute) + ",Name")]
    [NotNullNotEmpty("Необходимо выбрать атрибут")]
    public int AttributeId { get; set; } = 1;

    [Label("Атрибут")]
    [JsonIgnore()]
    [NotInput("Свойство "+nameof(Attribute) + " не вводится пользователем, оно устанавливается в соответвии с внешним ключом " + nameof(AttributeId))]
    public virtual MessageAttribute Attribute { get; set; }



    [Label("Протокол сообщения")]
    [SingleSelectApi(nameof(MessageProtocol) + ",Name")]
    //[NotNullNotEmpty("Необходимо выбрать протокол")]
    public int? MessageProtocolId { get; set; }
    
    [Label("Протокол сообщения")]
    [JsonIgnore()]
    [NotInput("Свойство " + nameof(MessageProtocol) + " не вводится пользователем, оно устанавливается в соответвии с внешним ключом " + nameof(MessageProtocolId))]
    public virtual MessageProtocol MessageProtocol { get; set; }


     
}
