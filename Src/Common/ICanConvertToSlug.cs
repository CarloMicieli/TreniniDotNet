namespace TreniniDotNet.Common
{
    public interface ICanConvertToSlug<T>
    {
        Slug ToSlug();
    }
}