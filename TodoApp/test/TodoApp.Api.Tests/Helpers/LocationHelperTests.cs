﻿using NUnit.Framework;
using System;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Hosting;
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
            _urlHelper.Route(Arg.Any<string>(), Arg.Any<Object>()).ReturnsForAnyArgs($"{route}{id}{postFix}");
            var expectedUri = new Uri($"{route}{id}{postFix}", UriKind.Relative);

            var result = _locationHelper.GetUriLocation(new Guid(id));

            Assert.That(result, Is.EqualTo(expectedUri));
        }
    }
}
