using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using TodoApp.Api.Models;

namespace TodoApp.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private static readonly Item ItemToPost =
            new Item {Text = "itemToPost", Id = new Guid("e6eb4638-38a4-49ac-8aaf-878684397707")};

        private static IEnumerable<Item> IteratedItems
        {
            get
            {
                yield return new Item {Text = "item0", Id = new Guid("e6eb4638-38a4-49ac-8aaf-878684397702")};
                yield return new Item {Text = "item1", Id = new Guid("a5d4b549-bdd3-4ec2-8210-ff42926aa141")};
                yield return new Item {Text = "item2", Id = new Guid("45c4fb8b-1cdf-42ca-8a61-67fd7f781057")};
            }
        }

        private static readonly Item[] Items = IteratedItems.ToArray();

        public async Task<IHttpActionResult> GetAsync()
            => await Task.FromResult(Ok(Items));

        public async Task<IHttpActionResult> GetAsync(Guid id)
            => await Task.FromResult(Ok(Items[0]));

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
        {
            var urlHelper = new UrlHelper(Request);
            var route = urlHelper.Route(RoutesConfig.ApiV2Route, new {id = "45c4fb8b-1cdf-42ca-8a61-67fd7f781057"});

            return await Task.FromResult(Created(new Uri(route, UriKind.Relative), ItemToPost));
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => await Task.FromResult(Content(HttpStatusCode.Accepted, Items[1]));

        public IHttpActionResult DeleteAsync(Guid id)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}