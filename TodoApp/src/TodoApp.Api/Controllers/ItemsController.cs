﻿using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using TodoApp.Database;
using TodoApp.Database.Models;

namespace TodoApp.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private readonly string _apiRoute;

        private readonly IItemRepository _repository;

        public ItemsController(IItemRepository repository)
        {
            _repository = repository;
            _apiRoute = RoutesConfig.ApiV1Route;
        }

        public async Task<IHttpActionResult> GetAsync()
            => await Task.FromResult(Ok(_repository.GetAll()));

        public async Task<IHttpActionResult> GetAsync(Guid id)
            => await Task.FromResult(Ok(_repository.GetById(id)));

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
        {
            var urlHelper = new UrlHelper(Request);
            var route = urlHelper.Route(_apiRoute, new {id = "45c4fb8b-1cdf-42ca-8a61-67fd7f781057"});

            return await Task.FromResult(Created(new Uri(route, UriKind.Relative), _repository.Add(item)));
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => await Task.FromResult(Content(HttpStatusCode.Accepted, _repository.Update(item)));

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
            => await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
    }
}