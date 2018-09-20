using System;
using ProductFinder.Csv;
using ProductFinder.Domain;
using Xunit;

namespace ProductFinder.Tests.Csv
{
    public class MusicContractCsvMapperTests
    {
        [Fact]
        public void WhenInputIsCorrect_ShouldMapAllFields()
        {
            var sut = new MusicContractCsvMapper();

            var result = sut.Map("Monkey Claw|Christmas Special|streaming|25th Dec 2012|31st Dec 2012".Split('|'));
            
            Assert.Equal("Monkey Claw", result.Artist);
            Assert.Equal("Christmas Special", result.Title);
            Assert.Collection(result.Usages, u => Assert.Equal(Usage.Streaming, u));
            Assert.Equal(new DateTime(2012, 12, 25), result.StartDate);
            Assert.Equal(new DateTime(2012, 12, 31), result.EndDate);
        }
    }
}