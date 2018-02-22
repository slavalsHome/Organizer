namespace Common.Collections
{
    public interface ICollectionItem<T>
        where T : class, ICollectionItem<T>, new()
    {
        SimpleCollection<T> ParentCollection { get; set; }  
    }
}
