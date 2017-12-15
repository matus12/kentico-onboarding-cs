using System;
using System.Net.Http;
using System.Web.Http.Routing;

namespace TodoApp.Api
{
    public class LocationHelper
    {
        private readonly UrlHelper _urlHelper;

        public LocationHelper(HttpRequestMessage requestMessage)
        {
            _urlHelper = new UrlHelper(requestMessage);
        }

        public Uri GetUriLocation(Guid id)
        {
            var route = _urlHelper.Route(RoutesConfig.ApiV1Route, new {id = id.ToString()});
            return new Uri(route, UriKind.Relative);
        }
    }
}