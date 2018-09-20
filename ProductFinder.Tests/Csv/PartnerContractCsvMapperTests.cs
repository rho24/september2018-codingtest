using System;
using ProductFinder.Csv;
using ProductFinder.Domain;
using Xunit;

namespace ProductFinder.Tests.Csv
{
    public class PartnerContractCsvMapperTests
    {
        [Fact]
        public void WhenInputIsCorrect_ShouldMapAllFields()
        {
            var sut = new PartnerContractCsvMapper();

            var result = sut.Map("ITunes|digital download".Split('|'));
            
            Assert.Equal("ITunes", result.Partner);
            Assert.Equal(Usage.DigitalDownload, result.Usage);
        }
    }
}