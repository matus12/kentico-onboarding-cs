using NUnit.Framework;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using NSubstitute;

namespace TodoApp.Api.Tests.Helpers
{
    [TestFixture]
    internal class LocationHelperTests
    {
        private string _id;

        [SetUp]
        public void SetUp()
        {
            _id = "5f1570b2-9e59-4281-9bf2-d5ee136ebf21";
        }

        [Test]
        public void GetUriLocation_newId_ReturnsCorrectUri()
        {
            var requestMessage = Substitute.For<HttpRequestMessage>();
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.Routes.MapHttpRoute(
                RoutesConfig.ApiV1Route,
                "api/v1/items/{id}"
            );
            requestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
            var locationHelper = new LocationHelper(requestMessage);
            var expectedUri = new Uri("/api/v1/items/" + _id, UriKind.Relative);

            var result = locationHelper.GetUriLocation(new Guid(_id));

            Assert.That(result, Is.EqualTo(expectedUri));
        }
    }
}
