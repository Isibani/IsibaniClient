using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsibaniClient.Data
{
    public class Product
    {  
        [Key]
        public int ProductId { get; set; }
        public IEnumerable<ClientProduct> ClientProducts { get; set; }
        public string  Name { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
    }
}
