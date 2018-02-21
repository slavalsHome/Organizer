namespace Common.Collections
{
    public interface ISelectedCollectonItem<T> : ICollectionItem<T> 
        where T : class, ICollectionItem<T>, new()
    {
        bool IsSelected { get; set; }
    }
}
