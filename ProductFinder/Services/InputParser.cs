using System;
using System.Linq;
using Baseline;
using ProductFinder.Domain;

namespace ProductFinder.Services
{
    public class InputParser : IInputParser
    {
        public FinderInputs ParseInputs(string input)
        {
            try
            {
                // Assume date is 3 parts
                var parts = input.Split(' ');

                if (parts.Length < 4)
                    throw new ArgumentException("Not enough input aprts");

                var partnerParts = parts.Take(parts.Length - 3);
                var partnerName = partnerParts.Join(" ");

                var dateParts = parts.TakeLast(3);
                var date = dateParts.Join(" ");

                return new FinderInputs
                {
                    PartnerName = partnerName,
                    Date = MappingHelpers.ParseDate(date)
                };
            }
            catch (Exception e)
            {
                throw new ArgumentException("Input should be in the format 'partner name 1st Jun 2012'", nameof(input),
                    e);
            }
        }
    }
}