using ProductFinder.Domain;

namespace ProductFinder.Csv
{
    public class PartnerContractCsvMapper : ICsvMapper<PartnerContract>
    {
        public PartnerContract Map(string[] parts)
        {
            return new PartnerContract
            {
                Partner = parts[0],
                Usage = MappingHelpers.ParseUsages(parts[1])[0]
            };
        }
    }
}