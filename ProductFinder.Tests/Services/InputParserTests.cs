using System;
using ProductFinder.Services;
using Xunit;

namespace ProductFinder.Tests.Services
{
    public class InputParserTests
    {
        [Fact]
        public void ParseInputs_ShouldThrowIfNotEnoughBits()
        {
            var sut = new InputParser();

            Assert.Throws<ArgumentException>(() => sut.ParseInputs("two words"));
        }
        
        [Fact]
        public void ParseInputs_ShouldThrowIfDateIsInvalid()
        {
            var sut = new InputParser();

            Assert.Throws<ArgumentException>(() => sut.ParseInputs("Partner name this is not a date"));
        }
        
        [Fact]
        public void ParseInputs_ShouldExtractPartner()
        {
            var sut = new InputParser();

            var result = sut.ParseInputs("Partner name 1st Jun 2012");

            Assert.Equal("Partner name", result.PartnerName);
        }
        
        [Fact]
        public void ParseInputs_ShouldExtractDate()
        {
            var sut = new InputParser();

            var result = sut.ParseInputs("Partner name 1st Jun 2012");

            Assert.Equal(new DateTime(2012, 6, 1), result.Date);
        }
    }
}