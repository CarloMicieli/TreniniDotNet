namespace TreniniDotNet.SharedKernel.Slugs
{
    public interface ICanConvertToSlug<T>
    {
        Slug ToSlug();
    }
}