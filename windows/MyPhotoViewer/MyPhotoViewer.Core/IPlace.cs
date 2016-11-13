namespace MyPhotoViewer.Core
{
    public interface IPlace
    {
        int Id { get; }
        string Name { get; }
        string City { get; }
        string Country { get; }
    }
}
