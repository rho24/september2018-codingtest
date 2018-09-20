using System;
using System.Linq;
using ProductFinder.Domain;
using ProductFinder.Repositories;
using Xunit;

namespace ProductFinder.Tests.Repositories
{
    public class PartnerContractsRepositoryTests
    {
        [Fact]
        public void GetAllAfterLoad_ShouldReturnAllData()
        {
            var sut = new PartnerContractRepository();
            
            sut.Load(new[]
            {
                new PartnerContract()
                {
                    Partner = "Partner 1",
                    Usage = Usage.DigitalDownload
                },
                new PartnerContract()
                {
                    Partner = "Partner 2",
                    Usage = Usage.Streaming
                },
            });

            var results = sut.GetAll().ToArray();

            Assert.Equal(2, results.Count());
            Assert.Equal("Partner 1", results[0].Partner);
            Assert.Equal("Partner 2", results[1].Partner);
        }
    }
}