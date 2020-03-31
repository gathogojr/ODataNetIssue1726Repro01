using ODataNetIssue1726Repro01.Models;
using Microsoft.OData;
using Microsoft.OData.Client;
using System;
using System.Collections.Generic;

namespace ODataNetIssue1726Repro01.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceUri = new Uri("http://localhost:61317/odata");
            var context = new Default.Container(serviceUri);

            // context.Products.ByKey(1).GetValueAsync().Wait();

            var product = new Product
            {
                Id = 99,
                Name = "Product 99"
            };

            context.Configurations.RequestPipeline.OnEntryStarting(args =>
            {
                if (product.GetType().IsAssignableFrom(args.Entity.GetType()))
                {
                    List<ODataProperty> entryProperties = args.Entry.Properties as List<ODataProperty>;
                    entryProperties.Add(new ODataProperty
                    {
                        Name = "SalesDescription",
                        Value = new ODataPrimitiveValue("Product 99 sales description")
                    });
                }
            });

            context.AddToProducts(product);
            context.SaveChangesAsync(SaveChangesOptions.None).Wait();
        }
    }
}
