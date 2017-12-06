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
            new Item("itemToPost", new Guid("e6eb4638-38a4-49ac-8aaf-878684397707"));

        private static IEnumerable<Item> IteratedItems
        {
            get
            {
                yield return new Item("item0", new Guid("e6eb4638-38a4-49ac-8aaf-878684397702"));
                yield return new Item("item1", new Guid("a5d4b549-bdd3-4ec2-8210-ff42926aa141"));
                yield return new Item("item2", new Guid("45c4fb8b-1cdf-42ca-8a61-67fd7f781057"));
            }
        }

        private static readonly Item[] Items = IteratedItems.ToArray();

        // GET api/<controller>
        public async Task<IHttpActionResult> GetAsync()
            => await Task.FromResult(Ok(Items));

        // GET api/<controller>/5
        public async Task<IHttpActionResult> GetAsync(Guid id)
            => await Task.FromResult(Ok(Items[0]));

        // POST api/<controller>
        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
        {
            UrlHelper helper = new UrlHelper(Request);
            var route = helper.Route("DefaultApi", new {id = "45c4fb8b-1cdf-42ca-8a61-67fd7f781057" });
            
            return await Task.FromResult(Created(new Uri(route, UriKind.Relative), ItemToPost));
        } 

        // PUT api/<controller>/5
        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => await Task.FromResult(Content(HttpStatusCode.Accepted, Items[1]));

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
            => await Task.FromResult(Ok());
    }
}