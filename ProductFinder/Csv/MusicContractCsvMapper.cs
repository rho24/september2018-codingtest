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
                Usages = ParseUsages(parts[2]),
                StartDate = ParseDate(parts[3]),
                EndDate = string.IsNullOrEmpty(parts[4]) ? null : (DateTime?) ParseDate(parts[4])
            };
        }

        private Usage[] ParseUsages(string value)
        {
            var parts = value.Split(',');

            return parts.Select(p =>
                    p.Trim() == "digital download" ? (Usage?) Usage.DigitalDownload :
                    p.Trim() == "streaming" ? (Usage?) Usage.Streaming : null)
                .Where(u => u.HasValue)
                .Select(u => u.Value)
                .ToArray();
        }

        private static DateTime ParseDate(string value)
        {
            var parts = value.Split(' ');

            var day = parts[0]
                .Replace("st", "")
                .Replace("nd", "")
                .Replace("rd", "")
                .Replace("th", "");

            var cleanerDate = $"{day} {parts[1]} {parts[2]}";

            return DateTime.Parse(cleanerDate);
        }
    }
}