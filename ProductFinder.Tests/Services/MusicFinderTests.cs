﻿using System;
using FakeItEasy;
using ProductFinder.Domain;
using ProductFinder.Repositories;
using ProductFinder.Services;
using Xunit;

namespace ProductFinder.Tests.Services
{
    public class MusicFinderTests
    {
        private MusicFinder _sut;
        private IPartnerContractRepository _partnerContractRepo;
        private IMusicContractsRepository _musicContractRepo;

        public MusicFinderTests()
        {
            _partnerContractRepo = A.Fake<IPartnerContractRepository>();
            _musicContractRepo = A.Fake<IMusicContractsRepository>();
            _sut = new MusicFinder(_partnerContractRepo, _musicContractRepo);
        }

        [Fact]
        public void FindContracts_ShouldThrowIfPartnerDoesntExist()
        {
            A.CallTo(() => _partnerContractRepo.GetByPartnerName("partner")).Returns(null);

            var ex = Assert.Throws<ArgumentException>(() => _sut.FindContracts("partner", new DateTime(2012, 6, 1)));
        }

        [Fact]
        public void FindContracts_ShouldGetContractsForUsageAndDateAndMapUsage()
        {
            A.CallTo(() => _partnerContractRepo.GetByPartnerName("partner"))
                .Returns(new PartnerContract
                {
                    Partner = "partner",
                    Usage = Usage.Streaming
                });
            
            var contracts = new[]
            {
                new MusicContract
                {
                    Artist = "Bob Dylan",
                    Title = "Blowing in the wind",
                    Usages = new[] {Usage.Streaming, Usage.DigitalDownload},
                    StartDate = new DateTime(2012, 01, 01),
                    EndDate = new DateTime(2012, 12, 31),
                },
            };

            A.CallTo(() => _musicContractRepo.GetForUsageAndDate(Usage.Streaming, new DateTime(2012, 6, 1)))
                .Returns(contracts);

            var result = _sut.FindContracts("partner", new DateTime(2012, 6, 1));

            Assert.Collection(result, c =>
            {
                Assert.Equal("Bob Dylan", c.Artist);
                Assert.Equal("Blowing in the wind", c.Title);
                Assert.Collection(c.Usages, u => Assert.Equal(Usage.Streaming, u));
                Assert.Equal(new DateTime(2012, 01, 01), c.StartDate);
                Assert.Equal(new DateTime(2012, 12, 31), c.EndDate);
            });
        }
    }
}