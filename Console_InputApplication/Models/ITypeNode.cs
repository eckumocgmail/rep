using System.Collections.Concurrent;

public interface ITypeNode<T> : TreeWalker<T>
{
    ConcurrentDictionary<string, TypeNode<T>> HierElements { get; set; }
    void ForEach(Action<TypeNode<T>> todo);
    T NodeItem { get; set; }
    string NodeName { get; set; }
    TypeNode<T> Parent { get; set; }

    TypeNode<T> Append(TypeNode<T> pchild);
    string GetIntPath();
    int GetLevel();
    List<string> GetPath();
    string GetPath(string separator);
    bool Has(string name);
    bool Remove(string name);

    List<TypeNode<T>> GetChildren();
}
