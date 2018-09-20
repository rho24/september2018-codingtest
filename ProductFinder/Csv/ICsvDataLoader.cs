using System.Collections.Generic;

namespace ProductFinder.Csv
{
    public interface ICsvDataLoader
    {
        IEnumerable<T> LoadData<T>(string csvFilePath, ICsvMapper<T> mapper);
    }
}