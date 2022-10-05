using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenHack2022.Application;
using OpenHack2022.Infrastructure;
using OpenHack2022.Models;
using System;
using System.Threading.Tasks;

namespace OpenHack2022.Tests
{
    [TestClass]
    public class RatingServiceTests
    {
        [TestMethod]
        public async Task CreateRatingShouldHaveWellFormedJSON()
        {
            var serviceUnderTest = StubService();
            string badSubmit = "}";
            var result = await serviceUnderTest.AddRating(badSubmit);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(6)]
        public async Task RatingShouldBeOneToFive(int ratingNumber)
        {
            var serviceUnderTest = StubService();
            var ratingUnderTest = new Rating(Guid.NewGuid(), Guid.NewGuid(), string.Empty, ratingNumber, string.Empty);
            var result = await serviceUnderTest.AddRating(ratingUnderTest);
            Assert.IsFalse(result.Success);
        }

        private static RatingService StubService()
        {
            var repoMock = new Mock<IRatingRepository>();
            var loggerMock = new Mock<ILogger<RatingService>>();

            return new RatingService(loggerMock.Object, repoMock.Object);

        }
    }
}
