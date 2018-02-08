using NUnit.Framework;
using System;
using System.Web.Http.Routing;
using NSubstitute;
using TodoApp.Api.Helpers;

namespace TodoApp.Api.Tests.Helpers
{
    [TestFixture]
    internal class LocationHelperTests
    {
        private UrlHelper _urlHelper;
        private LocationHelper _locationHelper;

        [SetUp]
        public void SetUp()
        {
            _urlHelper = Substitute.For<UrlHelper>();

            _locationHelper = new LocationHelper(_urlHelper);
        }

        [Test]
        public void GetUriLocation_NewId_ReturnsCorrectUri()
        {
            const string route = "/api/v1/items/";
            const string postFix = "/elephant";
            const string id = "5f1570b2-9e59-4281-9bf2-d5ee136ebf21";
            _urlHelper.Route("ApiV1",
                    Arg.Is<object>(value => (Guid) new HttpRouteValueDictionary(value)["id"] == new Guid(id)))
                .Returns($"{route}{id}{postFix}");
            var expectedUri = new Uri($"{route}{id}{postFix}", UriKind.Relative);

            var result = _locationHelper.GetUriLocation(new Guid(id));

            Assert.That(result, Is.EqualTo(expectedUri));
        }
    }
}