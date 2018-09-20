using ProductFinder.Domain;

namespace ProductFinder.Csv
{
    public interface ICsvMapper<T>
    {
        T Map(string[] parts);
    }
}