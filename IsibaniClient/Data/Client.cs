
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IsibaniClient.Data
{
    public class Client 
    {
        [Key]
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateLastEdited { get; set; }
        public bool Deleted { get; set; } = false;
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser IdentityUser { get; set; }
        public IEnumerable<ClientProduct> ClientProducts { get; set; }




    }
  


}


