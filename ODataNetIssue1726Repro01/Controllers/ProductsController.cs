using ODataNetIssue1726Repro01.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ODataNetIssue1726Repro01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        [EnableQuery]
        public IQueryable<Product> Get()
        {
            return new[] { new Product { Id = 1, Name = "Product 1" } }.AsQueryable();
        }

        [EnableQuery]
        public SingleResult<Product> Get([FromODataUri]int key)
        {
            return SingleResult.Create(new[] { new Product { Id = key, Name = "Product " + key } }.AsQueryable());
        }

        public async Task<IActionResult> Post([FromBody]Product product)
        {
            await Task.Run(() =>
            {
                // ...            
            });

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{product.Id}", UriKind.Absolute), product);
        }
    }
}
