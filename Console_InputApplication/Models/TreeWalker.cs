public interface TreeWalker<T>
{
    public void GoToParent(Action<ITypeNode<T>> handle);
    public void GoByLevels(Action<ITypeNode<T>> handle);
    public void GoFromChildren(Action<ITypeNode<T>> handle);
    public void GoToChildren(Action<ITypeNode<T>> handle);
}
