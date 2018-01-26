using NUnit.Framework;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using NSubstitute;
using TodoApp.Api.Helpers;

namespace TodoApp.Api.Tests.Helpers
{
    [TestFixture]
    internal class LocationHelperTests
    {
        private HttpRequestMessage _requestMessage;
        private LocationHelper _locationHelper;

        [SetUp]
        public void SetUp()
        {
            _requestMessage = Substitute.For<HttpRequestMessage>();
            _locationHelper = new LocationHelper(_requestMessage);
        }

        [Test]
        public void GetUriLocation_NewId_ReturnsCorrectUri()
        {
            const string id = "5f1570b2-9e59-4281-9bf2-d5ee136ebf21";
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.Routes.MapHttpRoute(
                RoutesConfig.ApiV1Route,
                "api/v1/items/{id}/elephant"
            );
            _requestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
            var expectedUri = new Uri("/api/v1/items/" + id + "/elephant", UriKind.Relative);

            var result = _locationHelper.GetUriLocation(new Guid(id));

            Assert.That(result, Is.EqualTo(expectedUri));
        }
    }
}
