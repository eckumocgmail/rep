using System.Collections.Concurrent;

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
