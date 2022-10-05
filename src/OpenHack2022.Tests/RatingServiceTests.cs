using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenHack2022.Application;
using OpenHack2022.Infrastructure;
using OpenHack2022.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenHack2022.Tests
{
    [TestClass]
    public class RatingServiceTests
    {
        [TestMethod]
        public void CreateRatingShouldHaveWellFormedJSON()
        {
            var serviceUnderTest = StubService();
            string badSubmit = "}";
            var result = serviceUnderTest.AddRating(badSubmit);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(6)]
        public void RatingShouldBeOneToFive(int ratingNumber)
        {
            var serviceUnderTest = StubService();
            var ratingUnderTest = new RatingModel(Guid.NewGuid(), Guid.NewGuid(), string.Empty, ratingNumber, string.Empty);
            var result = serviceUnderTest.AddRating(ratingUnderTest);
            Assert.IsFalse(result.Success);
        }

        private static RatingService StubService()
        {
            var repoMock = new Mock<IRatingRepository>();
            var loggerMock = new Mock<ILogger<RatingService>>();
            var handlerMock = new Mock<HttpMessageHandler>();

            var httpClient = new HttpClient(handlerMock.Object);

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@""),
            };

            return new RatingService(loggerMock.Object, repoMock.Object, httpClient);

        }
    }
}
