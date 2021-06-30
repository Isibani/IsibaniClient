using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsibaniClient.Data
{
    public class ClientProduct
    {
        [Key]
        public int ClientProducId { get; set; }
        public int ClientId  { get; set; }
        public virtual Client Client { get; set; }
        public int  ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Estimatedbudget { get; set; }
        public int  Amountspent { get; set; }
        public int Duration { get; set; }
         

    }
}
