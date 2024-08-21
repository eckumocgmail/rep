using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[Label("Тестирование функций определения функций вокруг атрибутов ввода")]
public class AttributesInputTest : TestingElement
{
    public AttributesInputTest()    {    }
    public AttributesInputTest(TestingUnit parent) : base(parent)    {    }

    public override void OnTest()

    {
        var model = new TextModel();
        try
        {
            model.EnsureIsValide();
            this.Messages.Add("Атрибуты ввода работают корректно");
        }
        catch(Exception ex)
        {
            this.Error($"{TypeExtensions2.GetTypeName(GetType())} не выполнен", ex);
            this.Messages.Add("Атрибуты ввода работают некорректно");
        }
        
    }





    public class TextModel : MyValidatableObject
    {


        public string Name { get; set; }

        [InputAriphmetic()] public string Ariphmetic { get; set; }
        [InputEmail()] public string Email { get; set; } = "gmail";
        [InputEngText()] public string EngText { get; set; }
        [InputEngWord()] public string EngWord { get; set; }
        [InputMultilineText()] public string MultilineText { get; set; }
        [InputPassword()] public string Password { get; set; }
        [InputConfirmation("Password")] public string Confirmation { get; set; }
        [InputPhone()] public string Phone { get; set; }
        [InputPunctuation()] public string Punctuation { get; set; }
        [InputRusText()] public string InputRusText { get; set; }
        [InputRusWord()] public string RusWord { get; set; }
        [InputTcpIp4Address()] public string TcpIp4Address { get; set; }
        [InputText()] public string Text { get; set; }
        [InputUrl()] public string Url { get; set; }
        [InputXml()] public string Xml { get; set; }

        public TextModel() : base()
        {

        }
    }



    public class NumberModel : MyValidatableObject
    {
        [InputPositiveInt] public int PositiveInt { get; set; }
        [InputInt] public int Int { get; set; }
        [InputPercent] public int Percent { get; set; }
        [InputDecimal] public int Decimal { get; set; }
        public NumberModel()
        {

        }
    }
    public class NumberTextModel : MyValidatableObject
    {
        [InputPositiveInt] public string PositiveInt { get; set; }
        [InputPositiveInt] public string Int { get; set; }
        [InputPercent] public string Percent { get; set; }
        [InputDecimal] public string Decimal { get; set; }
        public NumberTextModel()
        {

        }
    }
    public class DateModel : MyValidatableObject
    {
        [InputYear] public int Year { get; set; }
        [InputMonth] public int Month { get; set; }
        [InputDate] public int Date { get; set; }
        [InputTime] public int Time { get; set; }
        [InputWeek] public int Week { get; set; }
        [InputDateTime] public int DateTime { get; set; }
        public DateModel()
        {

        }
    }
    public class CustomModel : MyValidatableObject
    {
        [InputBoolAttribute] public bool Bool { get; set; }
        [InputColor] public string Color { get; set; }
        [InputCreditCard] public string CreditCard { get; set; }
        [InputCurrency] public float Currency { get; set; }
        [InputWeek] public int Week { get; set; }
        [InputFile] public byte[] File { get; set; }
        [InputFilePath] public string FilePath { get; set; }
        [InputHidden] public string Hidden { get; set; }
        [InputIcon] public string Icon { get; set; }
        [InputImage] byte[] Image { get; set; } = new byte[0];
        public CustomModel()
        {

        }
    }

    public class CollectionModel : MyValidatableObject
    {
        [Label("Список текстовых сообщений")]
        [InputPrimitiveCollection()]
        public List<string> ListString { get; set; }


        [Label("Список рассылки")]
        [InputPrimitiveCollection()]
        [InputEmail]
        public List<string> ListEmails { get; set; }

        [Label("Модели данных")]
        [InputStructureCollection(nameof(DateModel))] 
        public List<DateModel> ListDateModel { get; set; }

        public CollectionModel()
        {

        }
    }
}
 

 