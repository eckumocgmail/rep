 

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;

using Console_InputApplication;


public interface ITypeNode<T> : TreeWalker<T>
{
    ConcurrentDictionary<string, ITypeNode<T>> HierElements { get; set; }
    void ForEach(Action<ITypeNode<T>> todo);
    T NodeItem { get; set; }
    string NodeName { get; set; }
    ITypeNode<T> Parent { get; set; }

    ITypeNode<T> Append(ITypeNode<T> pchild);
    string GetIntPath();
    int GetLevel();
    List<string> GetPath();
    string GetPath(string separator);
    bool Has(string name);
    bool Remove(string name);

    List<ITypeNode<T>> GetChildren();
}


public interface TreeWalker<T>
{
    public void GoToParent(Action<ITypeNode<T>> handle);
    public void GoByLevels(Action<ITypeNode<T>> handle);
    public void GoFromChildren(Action<ITypeNode<T>> handle);
    public void GoToChildren(Action<ITypeNode<T>> handle);
}

/// <summary>
/// Иерархическая структура данных
/// </summary>
/// <typeparam name="T"></typeparam>
public class TypeNode<T> : IDictionary<string, ITypeNode<T>>, ITypeNode<T> where T : class
{
    /// <summary>
    /// Уникальное имя обьекта в родительском контексте
    /// </summary>
    public string NodeName { get; set; }

    /// <summary>
    /// Элемент
    /// </summary>
    public T NodeItem { get; set; }

    /// <summary>
    /// Дочерние элементы
    /// </summary>
    public IDictionary<string, ITypeNode<T>> ChildNodes { get; set; }

    public TypeNode()
    {
        this.NodeItem = null;// (T)typeof(T).GetConstructors().First(c => c.GetParameters().Count() == 0).Invoke(new object[0]);
        this.NodeName = typeof(T).GetLabel();
        this.Parent = null;
    }
    public TypeNode(ITypeNode<T> parent)
    {
        this.NodeItem = null;// (T)typeof(T).GetConstructors().First(c => c.GetParameters().Count() == 0).Invoke(new object[0]);
        this.NodeName = typeof(T).GetLabel();
        this.Parent = parent;
    }

    public void ForEach(Action<ITypeNode<T>> todo)
    {
        todo(this);
        GetChildren().ForEach( child => child.ForEach(todo) );
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="name"></param>
    /// <param name="item"></param>
    /// <param name="parent"></param>
    public TypeNode(string name, T item, ITypeNode<T> parent)
    {
        if (name == null)
        {
            throw new ArgumentNullException("name");
        }
        if (item == null)
        {
            throw new ArgumentNullException("item");
        }
        NodeName = name;
        NodeItem = item;
        Parent = parent;
        ChildNodes = new SortedDictionary<string, ITypeNode<T>>();
    }


    /// <summary>
    /// Удаление дочернего элемента
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool Remove(string name)
    {
        ITypeNode<T> output;
        return ChildNodes.Remove(name, out output);
    }


    /// <summary>
    /// Проверка наличия потомка с заданным именем
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool Has(string name)
    {
        return ChildNodes.ContainsKey(name);
    }


    /// <summary>
    /// Добавление потомка
    /// </summary>
    /// <param name="pchild"></param>
    /// <returns></returns>
    public ITypeNode<T> Append(ITypeNode<T> pchild)
    {
        if (pchild == null)
        {
            throw new ArgumentNullException("pchild");
        }
        if (Has(pchild.NodeName))
        {
            throw new Exception($"Обьект с именем {pchild.NodeName} уже зарегистрирован в узле: {GetPath()}");
        }
        else
        {

            return ChildNodes[pchild.NodeName] = pchild;
        }
    }


    /// <summary>
    /// Ссылка на родительский элемент
    /// </summary>
    /// 
    [JsonIgnore]
    private ITypeNode<T> _Parent { get; set; }

    public List<ITypeNode<T>> GetChildren()
        => new List<ITypeNode<T>>(this.ChildNodes.Values);

    /// <summary>
    /// Перемещение узла
    /// </summary>
    [JsonIgnore]
    public ITypeNode<T> Parent
    {
        get
        {
            return _Parent;
        }
        set
        {
            if (_Parent != null)
            {
                _Parent.Remove(NodeName);
            }
            if (value != null)
            {
                _Parent = value;
                _Parent.Append(this);
            }

        }
    }


    /// <summary>
    /// Получение глубины иерархии
    /// </summary>
    /// <returns></returns>
    public int GetLevel()
    {
        int level = 1;
        ITypeNode<T> p = this;
        while (p.Parent != null)
        {
            p = p.Parent;
            level++;
        }
        return level;
    }

    /// <summary>
    /// Получение пути от истока
    /// </summary>
    /// <returns></returns>
    public List<string> GetPath()
    {

        if (Parent != null)
        {
            List<string> path = Parent.GetPath();
            path.Add(NodeName);

            path.ForEach(id => Console.Write($"/{id}")); this.Info("");
            return path;
        }
        return new List<string> { NodeName };
    }




    /// <summary>
    /// Получение абсолюного идентификатора
    /// </summary>
    /// <param name="separator">разделитель</param>
    /// <returns></returns>
    public string GetPath(string separator)
    {
        string path = "";
        foreach (string name in GetPath())
        {
            if (path.Length != 0)
            {
                path += separator + name;
            }
            else
            {
                path = name;
            }
        }
        return path;
    }


    /// <summary>
    /// Обработка узлов поддерева вертикально вниз
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <param name="handle"></param>
    public void GoToChildren(Action<ITypeNode<T>> handle)
    {
        handle(this);
        foreach (ITypeNode<T> pchild in ChildNodes.Values)
        {
            pchild.GoToChildren(handle);
        }
    }


    /// <summary>
    /// Обработка узлов поддерева снизу вверх
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <param name="handle"></param>
    public void GoFromChildren(Action<ITypeNode<T>> handle)
    {
        foreach (ITypeNode<T> pchild in ChildNodes.Values)
        {
            pchild.GoFromChildren(handle);
        }
        handle(this);
    }


    /// <summary>
    /// Обход всей иерархии
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <param name="handle"></param>
    public void GoToParent(Action<ITypeNode<T>> handle)
    {
        handle((ITypeNode<T>)this);
        if (Parent != null)
        {
            Parent.GoToParent(handle);
        }
    }


    /// <summary>
    /// Обработка узлов поддерева сверху вниз
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <param name="handle"></param>
    public void GoByLevels(Action<ITypeNode<T>> handle)
    {
        if (Parent == null)
        {
            handle((ITypeNode<T>)this);
        }
        foreach (ITypeNode<T> pchild in ChildNodes.Values)
        {
            handle((ITypeNode<T>)pchild);
        }
        foreach (ITypeNode<T> pchild in ChildNodes.Values)
        {
            pchild.GoByLevels(handle);
        }
    }


    public void ForEach(Action<object> todo)
    {

    }

    ConcurrentDictionary<string, ITypeNode<T>> ITypeNode<T>.HierElements { get; set; }

    public string GetIntPath()
    {
        if (this.Parent != null)
        {
            string path = this.Parent.GetIntPath() + "." + (Parent.GetChildren().IndexOf(this) + 1);
            this.Info($"{NodeName} => {SerializeObject(GetPath(), Formatting.Indented)} => {path}");

            Console.Write("\n");
            for (int i = 0; i < GetLevel(); i++)
                Console.Write("  ");


            return path;

        }
        else
        {
            return "1";
        }
    }

    private object SerializeObject(List<string> list, Formatting indented) => list.ToJson();

    public void Add(string key, ITypeNode<T> value) => this.ChildNodes[key] = value;

    public bool ContainsKey(string key) => ChildNodes.ContainsKey(key);


    public bool TryGetValue(string key, [MaybeNullWhen(false)] out ITypeNode<T> value)

       => ChildNodes.TryGetValue(key, out value);


    public ITypeNode<T> this[string key] { get => ChildNodes[key]; set => ChildNodes[key] = value; }

    public ICollection<string> Keys => ChildNodes.Keys;

    public ICollection<ITypeNode<T>> Values => ChildNodes.Values;

    public void Add(KeyValuePair<string, ITypeNode<T>> item)
    {
        ChildNodes.Add(item);
    }

    public void Clear()
    {
        ChildNodes.Clear();
    }

    public bool Contains(KeyValuePair<string, ITypeNode<T>> item)
    {
        return ChildNodes.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, ITypeNode<T>>[] array, int arrayIndex)
    {
        ChildNodes.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, ITypeNode<T>> item)
    {
        return ChildNodes.Remove(item);
    }

    public int Count => ChildNodes.Count;

    public bool IsReadOnly => ChildNodes.IsReadOnly;

    public IEnumerator<KeyValuePair<string, ITypeNode<T>>> GetEnumerator()
    {
        return ChildNodes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ChildNodes.GetEnumerator();
    }
}
