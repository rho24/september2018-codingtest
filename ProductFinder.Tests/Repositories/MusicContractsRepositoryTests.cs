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
                    Usages = new Usage[] {Usage.DigitalDownload, Usage.Streaming},
                    StartDate = new DateTime(2018, 1, 1),
                    EndDate = new DateTime(2018, 12, 31)
                },
                new MusicContract
                {
                    Artist = "Bob Dylan",
                    Title = "Like a rolling stone",
                    Usages = new Usage[] {Usage.DigitalDownload},
                    StartDate = new DateTime(2018, 1, 1),
                    EndDate = new DateTime(2018, 12, 31)
                },
            });

            var results = sut.GetAll().ToArray();

            Assert.Equal(2, results.Count());
            Assert.Equal("Lay lady lay", results[0].Title);
            Assert.Equal("Like a rolling stone", results[1].Title);
        }
    }
}