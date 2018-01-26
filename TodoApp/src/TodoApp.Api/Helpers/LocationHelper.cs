using System;
using System.Net.Http;
using System.Web.Http.Routing;
using TodoApp.Contracts.Helpers;

namespace TodoApp.Api.Helpers
{
    internal class LocationHelper : ILocationHelper
    {
        private readonly UrlHelper _urlHelper;

        public LocationHelper(UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public Uri GetUriLocation(Guid id)
            => new Uri(_urlHelper.Route(RoutesConfig.ApiV1Route, new {id}), UriKind.Relative);
    }
}