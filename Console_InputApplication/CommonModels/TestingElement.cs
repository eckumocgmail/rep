using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static InputConsole;
using System.Threading.Tasks;
using static InputConsole;
using Console_InputApplication;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Console_InputApplication.InputApplicationModule;
using Microsoft.EntityFrameworkCore;

public class TestingUnit : TestingElement
{
    public TestingUnit()
    {
    }

    public TestingUnit(TestingUnit parent) : base(parent)
    {
    }

    public TestingUnit(IServiceProvider parent) : base(parent)
    {
    }


    public override void OnTest()
    {
        
     
    }

    public IEnumerable<string> GetTestPlan( )
    {
        var result = new List<string>();
        result.Add(GetType().GetTypeName());
        result.AddRange(GetElementNames(1));
        return result;
    }


}


public abstract class TestingElement<T>: TestingElement where T : class
{
    public T Get() => provider.Get<T>();
}

public abstract class TestingElement: TestingReport
{
    public LinkedList<TestingElement> Children = new LinkedList<TestingElement>();

    protected IServiceProvider provider;

    protected TestingElement(IServiceProvider provider)
    {
        this.Name = this.GetType().GetLabel();
        this.provider = provider;
    }

    private IEnumerable<TestingElement> GetChildren() => Children;
    public void Push(TestingElement pchild) => Append(pchild);

    public global::TypeNode<global::TestingElement> ToTreeNode()
    {
        TypeNode<TestingElement> result = new TypeNode<TestingElement>(
            $"[{this.GetTypeName()}]: {this.GetType().GetLabel()}", this, null);
        foreach (TestingElement PChild in Children)
        {
            var ChildNode = PChild.ToTreeNode();            
            ChildNode.Parent = result;
        }        
        return result;
    }

    public IEnumerable<string> GetElementNames(int level)
    {
        var result = new List<string>();
        foreach (var p in Children)
        {
            string s = "";
            for (int i = 0; i < level; i++)
            {
                s += "  ";
            }
            result.Add(s + p.GetTypeName());
            result.AddRange(p.GetElementNames(level+1));
        }
        return result;
    }

    protected List<string> Messages = new List<string>();
  
    protected TestingElement Parent;

    public TestingElement( ) : base( )
    {
 
    }
 

    public TestingElement(TestingUnit parent)
    {
        Parent = parent;
    }


    public abstract void OnTest();
    public virtual void Test() { }
    public virtual TestingReport DoTest(bool interactive = true){
        this.Info($"Выполняем тест [{this.GetType().Name}] {this.GetType().GetLabel()}");
        this.Info($" {this.GetType().GetDescription()}");
        
        if (this.IsExtends(typeof(TestingUnit)))
        {
            this.Info(this.Children.Select(p => $"[{p.Name}] {p.GetType().GetLabel()}").ToJsonOnScreen());            
        }

        if (interactive)
            ConfirmContinue();
        
        TestingReport report = this;
        
        report.Messages = this.Messages;
        report.Messages.Add(this.GetType().GetDescription());
        report.Name = this.GetType().GetLabel();
        if( String.IsNullOrWhiteSpace(report.Name))
        {
            report.Name = GetTypeName(this.GetType());
        }
        try
        {
            report.Started = DateTime.Now;
            if (interactive) Clear();
            this.OnTest();
        }
        catch (Exception ex)
        {
            this.WriteYellowLine("При выполнении теста "+this.GetTypeName()+" проброшено исключение: ", ex);
            
            report.Failed = true;
            report.Messages.Add(ex.ToString());
            if(interactive)
                throw;
        }
        finally
        {
            report.Ended = DateTime.Now;
            this.Info(Messages.ToJsonOnScreen());
            if (interactive)
                ConfirmContinue();
            if ( (this.IsExtends(typeof(TestingUnit))) == false)
            {
                //if (interactive) InputConsole.ConfirmContinue( );
                if (interactive) InputConsole.Clear();
                this.Info(report.ToDocument());
                if (interactive) InputConsole.ConfirmContinue();

            }
            foreach (var p in GetChildren())
            { 
                if (interactive) Clear();
                p.Name = p.GetType().GetLabel();
                report.SubReports[p.Name] = p.DoTest(interactive);
                if (report.SubReports[p.Name].Failed)
                {
                    report.Failed = true;
                }
            }
            
        }
        return report;
    }

    
    public void Append(TestingElement pchild)
    {
        this.Children.AddLast(pchild);
        pchild.Parent = this;
    }

    private string GetTypeName(Type propertyType)
    {
        try
        {
            if (propertyType == null)
                throw new ArgumentNullException("type");
            string name = propertyType.Name;
            if (name == null) return "";
            if (name.IndexOf("`") != -1)
                name = name.Substring(0, name.IndexOf("`"));

            var arr = propertyType.GetGenericArguments();
            if (arr.Length > 0)
            {
                name += '<';
                foreach (var arg in arr)
                {
                    name += GetTypeName(arg) + ",";
                }
                name = name.Substring(0, name.Length - 1);
                name += '>';
            }
            return name;
        }
        catch (Exception ex)
        {
            throw new Exception(nameof(GetTypeName)+" => ",ex);
        }
       
    }

    /// <summary>
    /// Метод выполнение проверки функциональности объекта
    /// </summary>
    /// <typeparam name="T">Тип сервиса предоставляющего функциональность</typeparam>
    /// <param name="test">Функция проверки функций</param>
    /// <param name="success">Сообщение при успешной проверки</param>
    /// <param name="failed">Сообщение при провале проверки</param>
    public void AssertService<T>(Predicate<T> test, string success, string failed)
    {
        try
        {
            var service = provider.Get<T>();
            var result = test(service);
            this.Messages.Add(result? success: failed);
        }
        catch (DbUpdateException ex)
        {
            string message = "";
            Exception p = ex;
            while(p != null)
            {
                message+= p.Message;
                p = p.InnerException;
            }
            Failed = true;
            this.Messages.Add($"{failed}: {message}");
        }
        catch (Exception ex)
        {
            Failed = true;
            this.Messages.Add($"{failed}: {ex.Message}");
        }
    }

    public void Assert(Predicate<TestingElement> test, string success, string failed)
    {
        try
        {           
            var result = test(this);
            this.Messages.Add(result ? success : failed);
        }
        catch (DbUpdateException ex)
        {
            string message = "";
            Exception p = ex;
            while (p != null)
            {
                message += p.Message;
                p = p.InnerException;
            }
            Failed = true;
            this.Messages.Add($"{failed}: {message}");
        }
        catch (Exception ex)
        {
            this.Failed = true;
            this.Messages.Add($"{failed}: {ex.Message}");
        }
    }
}
 
