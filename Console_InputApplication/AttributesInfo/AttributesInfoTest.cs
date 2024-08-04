using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using static InputConsole;

[Label("Проверяет работоспособность получения информационных аттрибутов")]
public class AttributesInfoTest : TestingElement
{
    public AttributesInfoTest() { }
    public AttributesInfoTest(TestingUnit parent) : base(parent) {}

    [Label("Label")]
    [Description("Description")]
    [Icon("home")]
    [HelpMessage("Help")]
    public class Model
    {

        [Label("Label")]
        [Description("Description")]
        [Icon("home")]
        [HelpMessage("Help")]
        public byte[] Data { get; set; }

        [Label("Label")]
        [Description("Description")]
        [Icon("Icon")]
        [HelpMessage("Help")]
        public void Init()
        {

        }
    }


    [Label("Выполнение проверки")]
    public override void OnTest()
    {
         
        var documentation = new TypeDocumentation(typeof(Model));
        documentation.EnsureIsValide();
        Clear();

        if (String.IsNullOrEmpty(documentation.Icon) == false)
        {
            Messages.Add("Функция получения атрибута IconAttribute работает корректно");
        }
        else
        {
            Messages.Add("Функция получения атрибута IconAttribute не работает корректно");
        }

        if (String.IsNullOrEmpty(documentation.Label) == false)
        {
            Messages.Add("Функция получения атрибута LabelAttribute работает корректно");
        }
        else
        {
            Messages.Add("Функция получения атрибута LabelAttribute не работает корректно");
        }

        if (String.IsNullOrEmpty(documentation.Description) == false)
        {
            Messages.Add("Функция получения атрибута DescriptionAttribute работает корректно");
        }
        else
        {
            Messages.Add("Функция получения атрибута DescriptionAttribute не работает корректно");
        }

                            
        if (String.IsNullOrEmpty(documentation.Help) == false)
        {
            Messages.Add("Функция получения атрибута HelpAttribute работает корректно");
        }
        else
        {
            Messages.Add("Функция получения атрибута HelpAttribute не работает корректно");
        }                
    } 
}
 
