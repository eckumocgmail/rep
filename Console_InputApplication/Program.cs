using Console_InputApplication;
using Console_InputApplication.EntityFasade;

using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Console_InputApplicationTemp
{

    class Program
    {

        static void Main(  string[] args )
        {
            var unit = new WebAppUnit();
            unit.WebPages.Create(new WebPage()
            {
                Url = "test"               
            });
           
            //unit.WebPages.GetAll().ToJsonOnScreen().WriteToConsole();
            
             //CompareProgram.Test();
             InputConsole.Interactive();
            /*InputConsole.Input<MyActionModel>(
                typeof(MyActionModel).GetLabel(), 
                null, 
                ref args
            );*/

            //SearchFilesProgram.SearchFile("D:\\", "*.csproj", 3).Result.ToJsonOnScreen();
            
            //InputConsole.Start(ref args);
            
        }


        /*
        private static string SelectClass(string cs, string className)
        {

            string text = "";
            int n = 1;
            this.Info(cs);
            int i = cs.IndexOf("class " + className);
            if (i == -1)
                throw new ArgumentException(cs, className);
            while (cs[i] != '{')
            {
                Console.Clear();
                this.Info(cs.Substring(i));
                ++i;
            }
            ++i;
            while (n > 0 || cs.Length > i)
            {

                Console.Clear();
                Debug.WriteLine("N=" + n);

                //Debug.WriteLine(cs.Substring(i));
                text += cs[i];
                if (cs[i] == '{') n++;
                if (cs[i] == '}') n--;
                ++i;
            }
            return text;

        }

        public static string GetMethodCsCode(string cs, string className, string action)
        {
            try
            {
                InputConsole.Info(cs);

                string inner = SelectClass(cs, className);

                InputConsole.Clear();
                InputConsole.Info(inner);
                string code = SelectClass(inner, action);

                InputConsole.Clear();
                InputConsole.Info($"Метод {action} класса {className}");
                InputConsole.Info(code);
                return code;
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось прочитать метод {action} класса {className}", ex);
            }

        }
        private static string[] TestInputFunctions(string[] args)
        {

            this.Info(GetMethodCsCode(
                @"""
                public class Test
                {
                    public void Run()
                    {
                        this.Info(1);
                    }
                """,
                "Test", "Run"
            ));


            string title = "Выбери для теста";
            ProgramDialog.UserInteractive = true;
            InputConsole.InputString("test", val => new List<string>(), ref args);
            bool result = InputConsole.Confirm("Разрешите");

            IEnumerable<string> options = new List<string>() { "1", "2", "3" };
            InputConsole.Interactive();
            var items = InputConsole.CheckList(title, options, ref args);
            this.Info(items.ToJsonOnScreen());
            Console.ReadKey();
            var selected = ProgramDialog.SingleSelect(title, options, ref args);
            this.Info(selected);
            Console.ReadKey();

            this.Info(InputConsole.CheckList("Выбери", new string[] { "1", "2", "3", "4" }, ref args));
            return args;
        }*/
    }
}
