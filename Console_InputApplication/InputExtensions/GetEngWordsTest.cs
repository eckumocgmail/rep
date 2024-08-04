using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Label("Тестирование функции определения латинских слов")]
public class GetEngWordsTest: TestingElement
{
    private static string SomeText = "Нужно сделат так что бы, Вася не смог найти меня. " +
        "Для этого я поменял свой номер телефона на 7-904-334-11-24 после того как перешел с него на " +
        "номер 7-904-334-12-25, который я поларил Ане." +
        "\n It is necessary to make sure Vasya cannot find me. ";

    private static IEnumerable<string> SomeTextEngWords = new List<string>() {
        "It","is","necessary","to","make","sure","Vasya","cannot", "find","me",
    };
 

    public GetEngWordsTest()
    {
    }

    public GetEngWordsTest(TestingUnit parent) : base(parent)
    {
    }

    public override void OnTest()
    {
     
        Messages.Add("Реализованы функции проверки данных по атрибутам");
        try
        {
            var left = SomeTextEngWords.Select(w => w.ToUpper()).ToHashSet<string>();
            var right = SomeText.GetEngWords().Select(w => w.ToUpper()).ToHashSet<string>();
            left.ToJsonOnScreen().WriteToConsole();
            right.ToJsonOnScreen().WriteToConsole();
            if (left.Count() == right.Count() && left.Except(right).Count() == 0)
            {
                Messages.Add("Функция считывания латинских слов работает корректно");
            }
            else
            {
                Messages.Add("Функция считывания русских слов не работает корректно");
            }



        }
        catch (Exception ex)
        {
            this.Error(ex);
            Messages.Add("Функции считывания работают не корректно");

        }

         
    }

}