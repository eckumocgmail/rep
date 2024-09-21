using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Xml;

/// <summary>
/// Иерархическая структура данных
/// </summary>
/// <typeparam name="T"></typeparam>
public class TypeNode<T> : IDictionary<string, TypeNode<T>>
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
    public IDictionary<string, TypeNode<T>> ChildNodes { get; set; } = new Dictionary<string, TypeNode<T>>();

    public TypeNode()
    {
        // (T)typeof(T).GetConstructors().First(c => c.GetParameters().Count() == 0).Invoke(new object[0]);
        this.NodeName = typeof(T).GetLabel();
        this.Parent = null;
    }
    public TypeNode(TypeNode<T> parent)
    {
        // (T)typeof(T).GetConstructors().First(c => c.GetParameters().Count() == 0).Invoke(new object[0]);
        this.NodeName = typeof(T).GetLabel();
        this.Parent = parent;
    }

    public void ForEach(Action<TypeNode<T>> todo)
    {
        todo(this);
        GetChildren().ForEach(child => child.ForEach(todo));
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="name"></param>
    /// <param name="item"></param>
    /// <param name="parent"></param>
    public TypeNode(string name, T item, TypeNode<T> parent)
    {        
        if (item == null)
        {
            throw new ArgumentNullException("item");
        }
        NodeName = name;
        NodeItem = item;
        Parent = parent;
        ChildNodes = new SortedDictionary<string, TypeNode<T>>();
    }


    /// <summary>
    /// Удаление дочернего элемента
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool Remove(string name)
    {
        TypeNode<T> output;
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
    public TypeNode<T> Append(TypeNode<T> pchild)
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
    private TypeNode<T> _Parent { get; set; }

    public List<TypeNode<T>> GetChildren()
        => new List<TypeNode<T>>(this.ChildNodes.Values);

    /// <summary>
    /// Перемещение узла
    /// </summary>
    [JsonIgnore]
    public TypeNode<T> Parent
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
        TypeNode<T> p = this;
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
    public void GoToChildren(Action<TypeNode<T>> handle)
    {
        handle(this);
        foreach (TypeNode<T> pchild in ChildNodes.Values)
        {
            pchild.GoToChildren(handle);
        }
    }


    /// <summary>
    /// Обработка узлов поддерева снизу вверх
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <param name="handle"></param>
    public void GoFromChildren(Action<TypeNode<T>> handle)
    {
        foreach (TypeNode<T> pchild in ChildNodes.Values)
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
    public void GoToParent(Action<TypeNode<T>> handle)
    {
        handle((TypeNode<T>)this);
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
    public void GoByLevels(Action<TypeNode<T>> handle)
    {
        if (Parent == null)
        {
            handle((TypeNode<T>)this);
        }
        foreach (TypeNode<T> pchild in ChildNodes.Values)
        {
            handle((TypeNode<T>)pchild);
        }
        foreach (TypeNode<T> pchild in ChildNodes.Values)
        {
            pchild.GoByLevels(handle);
        }
    }


    public void ForEach(Action<object> todo)
    {

    }


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

    public void Add(string key, TypeNode<T> value) => this.ChildNodes[key] = value;

    public bool ContainsKey(string key) => ChildNodes.ContainsKey(key);


    public bool TryGetValue(string key, [MaybeNullWhen(false)] out TypeNode<T> value)

       => ChildNodes.TryGetValue(key, out value);


    public TypeNode<T> this[string key] { get => ChildNodes[key]; set => ChildNodes[key] = value; }

    public ICollection<string> Keys => ChildNodes.Keys;

    public ICollection<TypeNode<T>> Values => ChildNodes.Values;

    public void Add(KeyValuePair<string, TypeNode<T>> item)
    {
        ChildNodes.Add(item);
    }

    public void Clear()
    {
        if (ChildNodes is not null)
        {
            ChildNodes.Clear();
        }
    }

    public bool Contains(KeyValuePair<string, TypeNode<T>> item)
    {
        return ChildNodes is null? false: ChildNodes.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, TypeNode<T>>[] array, int arrayIndex)
    {
        if(ChildNodes is null)
            throw new ArgumentNullException(nameof(ChildNodes));
        ChildNodes.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, TypeNode<T>> item)
    {
        return ChildNodes.Remove(item);
    }

    public int Count => ChildNodes.Count;

    public bool IsReadOnly => ChildNodes.IsReadOnly;

    public IEnumerator<KeyValuePair<string, TypeNode<T>>> GetEnumerator()
    {
        return ChildNodes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ChildNodes.GetEnumerator();
    }
}
