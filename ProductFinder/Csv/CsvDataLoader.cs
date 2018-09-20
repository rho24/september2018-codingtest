using System;
using System.Collections.Generic;
using System.IO;

namespace ProductFinder.Csv
{
    public class CsvDataLoader : ICsvDataLoader
    {
        public IEnumerable<T> LoadData<T>(string csvFilePath, ICsvMapper<T> mapper)
        {
            if (csvFilePath == null)
                throw new ArgumentNullException(nameof(csvFilePath));

            if (!File.Exists(csvFilePath))
                throw new ArgumentException("Csv file does not exist", nameof(csvFilePath));

            using (var reader = File.OpenText(csvFilePath))
            {
                var firstLine = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (firstLine)
                    {
                        firstLine = false;
                        continue;
                    }

                    if (!string.IsNullOrEmpty(line))
                    {
                        var parts = line.Split('|');

                        yield return mapper.Map(parts);
                    }
                }
            }
        }
    }
}