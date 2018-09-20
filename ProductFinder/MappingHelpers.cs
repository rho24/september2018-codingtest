using System;
using System.Linq;
using ProductFinder.Domain;

namespace ProductFinder
{
    public static class MappingHelpers
    {
        public static Usage[] ParseUsages(string value)
        {
            var parts = value.Split(',');

            return parts.Select(p =>
                    p.Trim() == "digital download" ? (Usage?) Usage.DigitalDownload :
                    p.Trim() == "streaming" ? (Usage?) Usage.Streaming : null)
                .Where(u => u.HasValue)
                .Select(u => u.Value)
                .ToArray();
        }

        public static DateTime ParseDate(string value)
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