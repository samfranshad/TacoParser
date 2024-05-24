using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldReturnNonNullObject()
        {
            var tacoParser = new TacoParser();

            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");

            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        [InlineData("33.47132,-84.4473,Taco Bell Fayetteville/1...", -84.4473)]
        [InlineData("34.719613,-86.578994,Taco Bell Huntsville...", -86.578994)]
        [InlineData("33.824114,-84.107251,Taco Bell Stone Mountai...", -84.107251)]
        [InlineData("34.2223,-84.503673,Taco Bell Canton...", -84.503673)]
        public void ShouldParseLongitude(string line, double expected)
        {
            var tacoParser = new TacoParser();

            var actual = tacoParser.Parse(line).Location.Longitude;

            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("34.992219, -86.841402, Taco Bell Ardmore...", 34.992219)]
        [InlineData("34.196869,-84.135598,Taco Bell Cumming...", 34.196869)]
        [InlineData("34.679723,-84.487535,Taco Bell East Ellija...", 34.679723)]
        [InlineData("34.057823,-84.592806,Taco Bell Kennesaw...", 34.057823)]
        [InlineData("33.375958,-84.569057,Taco Bell Peachtree Cit...", 33.375958)]
        public void ShouldParseLatitude(string line, double expected)
        {
            var tacoParser = new TacoParser();

            var actual = tacoParser.Parse(line).Location.Latitude;

            Assert.Equal(expected, actual);
        }

    }
}
