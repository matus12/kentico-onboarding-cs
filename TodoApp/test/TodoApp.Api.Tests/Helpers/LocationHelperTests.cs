using NUnit.Framework;
using System;
using System.Net.Http;
using NSubstitute;

namespace TodoApp.Api.Tests.Helpers
{
    [TestFixture]
    internal class LocationHelperTests
    {
        [Test]
        public void GetUriLocation_newId_ReturnsCorrectUri()
        {
            var requestMessage = Substitute.For<HttpRequestMessage>();
            var locationHelper = new LocationHelper(requestMessage);
            var newId = new Guid("5f1570b2-9e59-4281-9bf2-d5ee136ebf21");
            var expectedUri = new Uri("/api/v1/items/5f1570b2-9e59-4281-9bf2-d5ee136ebf21", UriKind.Relative);

            var result = locationHelper.GetUriLocation(newId);

            Assert.That(result, Is.EqualTo(expectedUri));
        }
    }
}
