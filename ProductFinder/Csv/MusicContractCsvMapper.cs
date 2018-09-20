using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using ProductFinder.Domain;

namespace ProductFinder.Csv
{
    public class MusicContractCsvMapper : ICsvMapper<MusicContract>
    {
        public MusicContract Map(string[] parts)
        {
            return new MusicContract
            {
                Artist = parts[0],
                Title = parts[1],
                Usages = MappingHelpers.ParseUsages(parts[2]),
                StartDate = MappingHelpers.ParseDate(parts[3]),
                EndDate = string.IsNullOrEmpty(parts[4]) ? null : (DateTime?) MappingHelpers.ParseDate(parts[4])
            };
        }
    }
}