using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODataNetIssue1726Repro01.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public IDictionary<string, object> DynamicProperties { get; set; }
    }
}
