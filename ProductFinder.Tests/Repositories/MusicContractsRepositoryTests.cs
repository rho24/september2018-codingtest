using System;
using System.Linq;
using ProductFinder.Domain;
using ProductFinder.Repositories;
using Xunit;

namespace ProductFinder.Tests.Repositories
{
    public class MusicContractsRepositoryTests
    {
        [Fact]
        public void GetAllAfterLoad_ShouldReturnAllData()
        {
            var sut = new MusicContractsRepository();
            
            sut.Load(new[]
            {
                new MusicContract
                {
                    Artist = "Bob Dylan",
                    Title = "Lay lady lay",
                    Usages = new [] {Usage.DigitalDownload, Usage.Streaming},
                    StartDate = new DateTime(2018, 1, 1),
                    EndDate = new DateTime(2018, 12, 31)
                },
                new MusicContract
                {
                    Artist = "Bob Dylan",
                    Title = "Like a rolling stone",
                    Usages = new [] {Usage.DigitalDownload},
                    StartDate = new DateTime(2018, 1, 1),
                    EndDate = new DateTime(2018, 12, 31)
                },
            });

            var results = sut.GetAll().ToArray();

            Assert.Collection(results,
                c => Assert.Equal("Lay lady lay", c.Title),
                c => Assert.Equal("Like a rolling stone", c.Title));
        }

        [Fact]
        public void GetForUsageAndDate_ShouldFIlterByUsage()
        {
            var sut = new MusicContractsRepository();
            
            sut.Load(new[]
            {
                new MusicContract
                {
                    Artist = "Bob Dylan",
                    Title = "Lay lady lay",
                    Usages = new [] {Usage.DigitalDownload, Usage.Streaming},
                    StartDate = new DateTime(2018, 1, 1),
                    EndDate = new DateTime(2018, 12, 31)
                },
                new MusicContract
                {
                    Artist = "Bob Dylan",
                    Title = "Like a rolling stone",
                    Usages = new [] {Usage.DigitalDownload},
                    StartDate = new DateTime(2018, 1, 1)
                },
            });

            var results1 = sut.GetForUsageAndDate(Usage.DigitalDownload, new DateTime(2018, 1, 1)).ToArray();
            var results2 = sut.GetForUsageAndDate(Usage.Streaming, new DateTime(2018, 1, 1)).ToArray();

            Assert.Collection(results1,
                c => Assert.Equal("Lay lady lay", c.Title),
                c => Assert.Equal("Like a rolling stone", c.Title));
            
            Assert.Collection(results2,
                c => Assert.Equal("Lay lady lay", c.Title));
        }

        [Fact]
        public void GetForUsageAndDate_ShouldFIlterByDate()
        {
            var sut = new MusicContractsRepository();
            
            sut.Load(new[]
            {
                new MusicContract
                {
                    Artist = "Bob Dylan",
                    Title = "Lay lady lay",
                    Usages = new [] {Usage.DigitalDownload},
                    StartDate = new DateTime(2018, 1, 1),
                    EndDate = new DateTime(2018, 12, 31)
                },
                new MusicContract
                {
                    Artist = "Bob Dylan",
                    Title = "Like a rolling stone",
                    Usages = new [] {Usage.DigitalDownload},
                    StartDate = new DateTime(2018, 1, 1)
                },
            });

            var resultsBeforeDates = sut.GetForUsageAndDate(Usage.DigitalDownload, new DateTime(2000, 1, 1)).ToArray();
            var resultsOnStartDate = sut.GetForUsageAndDate(Usage.DigitalDownload, new DateTime(2018, 1, 1)).ToArray();
            var resultsBetweenDates = sut.GetForUsageAndDate(Usage.DigitalDownload, new DateTime(2018, 6, 1)).ToArray();
            var resultsOnEndDate = sut.GetForUsageAndDate(Usage.DigitalDownload, new DateTime(2018, 12, 31)).ToArray();
            var resultsAfterEndDate = sut.GetForUsageAndDate(Usage.DigitalDownload, new DateTime(2020, 1, 1)).ToArray();

            Assert.Collection(resultsBeforeDates);
            
            Assert.Collection(resultsOnStartDate,
                c => Assert.Equal("Lay lady lay", c.Title),
                c => Assert.Equal("Like a rolling stone", c.Title));
            
            Assert.Collection(resultsBetweenDates,
                c => Assert.Equal("Lay lady lay", c.Title),
                c => Assert.Equal("Like a rolling stone", c.Title));
            
            Assert.Collection(resultsOnEndDate,
                c => Assert.Equal("Lay lady lay", c.Title),
                c => Assert.Equal("Like a rolling stone", c.Title));
            
            Assert.Collection(resultsAfterEndDate,
                c => Assert.Equal("Like a rolling stone", c.Title));
        }
    }
}