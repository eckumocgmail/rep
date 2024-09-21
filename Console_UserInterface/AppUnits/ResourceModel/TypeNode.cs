using Newtonsoft.Json;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;


/// <summary>
/// Иерархическая структура данных
/// </summary>
/// <typeparam name="T"></typeparam>
public class TreeNode<T>   
{
    /// <summary>
    /// Уникальное имя обьекта в родительском контексте
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Элемент
    /// </summary>
    public T Item { get; set; }

    /// <summary>
    /// Дочерние элементы
    /// </summary>
    public ConcurrentDictionary<string, TreeNode<T>> Children { get; set; }


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="name"></param>
    /// <param name="item"></param>
    /// <param name="parent"></param>
    public TreeNode( string name, T item, TreeNode<T> parent )
    {
        if (name == null)
        {
            throw new ArgumentNullException("name");
        }
        if (item == null)
        {
            throw new ArgumentNullException("item");
        }
        Name = name;
        Item = item;
        Parent = parent;
        Children = new ConcurrentDictionary<string, TreeNode<T>>();
    }


    /// <summary>
    /// Удаление дочернего элемента
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool Remove(string name)
    {
        TreeNode<T> output;
        return Children.TryRemove(name,out output);
    }


    /// <summary>
    /// Проверка наличия потомка с заданным именем
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool Has(string name)
    {
        return Children.ContainsKey(name);
    }


    /// <summary>
    /// Добавление потомка
    /// </summary>
    /// <param name="pchild"></param>
    /// <returns></returns>
    public TreeNode<T> Append(TreeNode<T> pchild)
    {
        if(pchild == null)
        {
            throw new ArgumentNullException("pchild");
        }
        if (Has(pchild.Name))
        {
            throw new Exception($"Обьект с именем {pchild.Name} уже зарегистрирован в узле: {GetPath()}");
        }
        else
        {
            return Children[pchild.Name] = pchild;
        }
    }


    /// <summary>
    /// Ссылка на родительский элемент
    /// </summary>
    /// 
    [JsonIgnore]
    private TreeNode<T> _Parent { get; set; }


    /// <summary>
    /// Перемещение узла
    /// </summary>
    [JsonIgnore]
    public TreeNode<T> Parent 
    {
        get
        {
            return _Parent;
        }
        set
        {
            if(_Parent !=null)
            {
                _Parent.Remove(Name);
            }
            if( value != null)
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
        TreeNode<T> p = this;
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
            path.Add(Name);
            return path;
        }
        return new List<string> { Name };
    }


    /// <summary>
    /// Получение абсолюного идентификатора
    /// </summary>
    /// <param name="separator">разделитель</param>
    /// <returns></returns>
    public string GetPath(string separator)
    {
        string path = "";
        foreach(string name in GetPath())
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
    public void DoBroadcastToHierElements( Action<object> handle )
    {
        handle(this);
        foreach(TreeNode<T> pchild in Children.Values)
        {
            pchild.DoBroadcastToHierElements(handle);
        }
    }


    /// <summary>
    /// Обработка узлов поддерева снизу вверх
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <param name="handle"></param>
    public void DoBroadcastFromHierElements<TNode>(Action<TNode> handle) where TNode : TreeNode<T>
    {            
        foreach (TreeNode<T> pchild in Children.Values)
        {
            pchild.DoBroadcastFromHierElements<TNode>(handle);
        }
        handle((TNode)this);
    }


    /// <summary>
    /// Обход всей иерархии
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <param name="handle"></param>
    public void DoBroadcastToParent<TNode>(Action<TNode> handle) where TNode : TreeNode<T>
    {
        handle((TNode)this);
        if(Parent!=null)
        {
            Parent.DoBroadcastToParent<TNode>(handle);
        }
    }


    /// <summary>
    /// Обработка узлов поддерева сверху вниз
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <param name="handle"></param>
    public void DoBroadcastToBrothers<TNode>(Action<TNode> handle) where TNode : TreeNode<T>
    {            
        if(Parent == null)
        {
            handle((TNode)this);
        }
        foreach (TreeNode<T> pchild in Children.Values)
        {
            handle((TNode)pchild);                
        }
        foreach (TreeNode<T> pchild in Children.Values)
        {
            pchild.DoBroadcastToBrothers<TNode>(handle);
        }            
    }


    
            
}




public class TypeFile : BaseEntity
{

    [NotNullNotEmpty("Необходимо ввести задать тип ресурса (MimeType)")]
    public string Mime { get; set; }


    [NotNullNotEmpty("Необходимо указать наименование ресурса")]
    public string Name { get; set; }

    [InputFile("*.*")]

    [NotNullNotEmpty("Необходимо ввести бинарные данные ресурса")]
    public byte[] Data { get; set; }


    [InputDateTime()]
    [NotNullNotEmpty("Необходимо указать время создания ресурса")]
    public DateTime Changed { get; set; }

}


/// <summary>
/// Модель директории в файловой системе.
/// При инициаллизации считывает все внутрении файлы.
/// </summary>
public class TypeCatalog : TreeNode<Dictionary<string, TypeFile>>
{
    /// <summary>
    /// Конструктор корня иерархии
    /// </summary>
    /// <param name="path"></param>
    /// <param name="rootName"></param>
    public TypeCatalog(string path, string rootName) : base(rootName, new Dictionary<string, TypeFile>(), null)
    {
        ReadFiles(path);
        foreach (string dir in System.IO.Directory.GetDirectories(path))
        {
            string name = dir.Substring(path.Length + 1);
            TypeCatalog subcatalog = new TypeCatalog(dir, name, this);
        }
    }

    /// <summary>
    /// Конструктор дочернего узла
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    private TypeCatalog(string path, string name, TypeCatalog parent) : base(name, new Dictionary<string, TypeFile>(), parent)
    {
        ReadFiles(path);
        foreach (string dir in System.IO.Directory.GetDirectories(path))
        {
            string childName = dir.Substring(path.Length + 1);
            TypeCatalog subcatalog = new TypeCatalog(dir, childName, this);
        }
    }


    /// <summary>
    /// Считывание файлов
    /// </summary>
    /// <param name="path">путь к каталогу</param>
    private void ReadFiles(string path)
    {
        foreach (string filepath in System.IO.Directory.GetFiles(path))
        {
            string name = filepath.Substring(path.Length + 1);
            int i = name.LastIndexOf(".");
            string ext = "text/plain";
            if (i != -1)
            {
                ext = name.Substring(i + 1).ToLower();
            }
            Item[name] = new TypeFile()
            {
                Name = name,
                Mime = $"text\\{ext}",
                Data = System.IO.File.ReadAllBytes(filepath),
                Changed = System.IO.File.GetLastWriteTime(filepath)
            };
        };
    }
}

