using System;
using System.Linq;
using ProductFinder.Domain;
using ProductFinder.Repositories;
using Xunit;

namespace ProductFinder.Tests.Repositories
{
    public class PartnerContractsRepositoryTests
    {
        private PartnerContractRepository _sut;

        public PartnerContractsRepositoryTests()
        {
            _sut = new PartnerContractRepository();
            _sut.Load(new[]
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
        }

        [Fact]
        public void GetAllAfterLoad_ShouldReturnAllData()
        {
            var results = _sut.GetAll().ToArray();

            Assert.Equal(2, results.Count());
            Assert.Equal("Partner 1", results[0].Partner);
            Assert.Equal("Partner 2", results[1].Partner);
        }

        [Fact]
        public void GetByPartnerName_ShouldReturnContractIfExists()
        {
            var result = _sut.GetByPartnerName("Partner 1");

            Assert.NotNull(result);
            Assert.Equal("Partner 1", result.Partner);
            Assert.Equal(Usage.DigitalDownload, result.Usage);
        }

        [Fact]
        public void GetByPartnerName_ShouldReturnNullIfDoesntExist()
        {
            var result = _sut.GetByPartnerName("Partner doesn't exist");

            Assert.Null(result);
        }
    }
}