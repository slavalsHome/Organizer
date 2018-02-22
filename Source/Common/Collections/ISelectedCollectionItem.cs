namespace Common.Collections
{
    public interface ISelectedCollectionItem : ICollectionItem
    {
        new ISelectedCollection ParentCollection { get; set; }  
        bool IsSelected { get; set; }
    }
}
