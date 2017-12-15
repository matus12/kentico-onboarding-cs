using System;
using System.Net.Http;
using System.Web.Http.Routing;
using TodoApp.Interfaces;

namespace TodoApp.Api
{
    public class LocationHelper : ILocationHelper
    {
        private readonly UrlHelper _urlHelper;

        public LocationHelper(HttpRequestMessage requestMessage)
        {
            _urlHelper = new UrlHelper(requestMessage);
        }

        public Uri GetUriLocation(Guid id)
            => new Uri(_urlHelper.Route(RoutesConfig.ApiV1Route, new {id}), UriKind.Relative);
    }
}