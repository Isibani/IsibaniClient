using IsibaniClient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsibaniClient.Models
{
    public class ClientDetailsViewModel
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateLastEdited { get; set; }
        public bool Deleted { get; set; } = false;
        public string UserId { get; set; }
        public int Estimatedbudget { get; set; }
        public int Amountspent { get; set; }
        public int Duration { get; set; }


        public IEnumerable<ClientProduct> ClientProducts { get; set; }
    }
}
