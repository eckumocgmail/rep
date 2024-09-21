using Microsoft.AspNetCore.WebSockets;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[Label("Программа выполнения методов тестирования")]
public class TestProgram: ConsoleProgram<TestRunner>
{
    [Label("Выбор элемента в иерархической структуре")]
    public TypeNode<TNodeItem> SelectTreeNode<TNodeItem>(string title, TypeNode<TNodeItem> root, Func<TNodeItem, string> print) where TNodeItem : class
    {
        TypeNode<TNodeItem> p = root;
        var completed = false;
        do
        {
            Console.Clear();
            root.ForEach(node =>
            {
                if (node == p)
                {
                    for (int i = 0; i < node.GetLevel(); i++) this.Info("    ");
                    this.WriteYellowLine(print(node.NodeItem));
                }
                else
                {
                    for (int i = 0; i < node.GetLevel(); i++) this.Info("    ");
                    this.WriteLine(print(node.NodeItem));
                }
            });
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Enter: ConfirmExecute("Сохранить и выйти?", () => completed = true); break;
                case ConsoleKey.DownArrow:
                {
                    if (p.Parent == null)
                    {
                        break;
                    }
                    else
                    {
                        int indexOfP = p.GetChildren().IndexOf(p);
                        if( indexOfP > (p.GetChildren().Count()-1))
                        {
                            break;
                        }
                        else
                        {
                            p = p.GetChildren()[indexOfP + 1];
                            break;
                        }                        
                    }
                }
                case ConsoleKey.UpArrow:        ConfirmExecute("Сохранить и выйти?", () => completed = true); break;
                case ConsoleKey.LeftArrow:      ConfirmExecute("Сохранить и выйти?", () => completed = true); break;
                case ConsoleKey.RightArrow:     ConfirmExecute("Сохранить и выйти?", () => completed = true); break;
                default: this.Info("Используйте стрелки клавиатуры для навигации и ENTER для завершения выбора"); break;
            }
        } while (completed==false);
        return p;
    }



}


[Label("Программа выполнения методов тестирования")]
public interface ITestRunner
{
    public IEnumerable<Type> GetElementTypes();
    public IEnumerable<Type> GetUnitTypes();
}



[Label("Программа выполнения методов тестирования")]
public class TestRunner: MyValidatableObject, ITestRunner
{
    private readonly Assembly _assembly;

    public TestRunner(){}
    
    public IEnumerable<Type> GetElementTypes()
        => _assembly.GetClassTypes().Where(t => t.IsExtendsFrom(typeof(TestingElement)));

    public IEnumerable<Type> GetUnitTypes()
        => _assembly.GetClassTypes().Where(t => t.IsExtendsFrom(typeof(TestingUnit)));

    public TestRunner(Assembly assembly=null)
    {
        _assembly = assembly==null? Assembly.GetExecutingAssembly(): assembly;
    }


    
    
}


public static class TestRunnerExtensions
{
    public static IEnumerable<Type> GetTestUnitTypes(this Assembly assembly)
        => assembly.GetClassTypes().Where(t => t.IsExtendsFrom(typeof(TestingUnit)));
    
}