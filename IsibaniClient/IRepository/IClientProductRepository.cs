using IsibaniClient.Data;
using IsibaniClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsibaniClient.IRepository
{
    public interface IClientProductRepository
    {
        IEnumerable<ClientProductViewModel> GetAll();
        ClientDetailsViewModel GetDetails(int ClientId=0);
    }
}
