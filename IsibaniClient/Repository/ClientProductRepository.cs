using IsibaniClient.Data;
using IsibaniClient.IRepository;
using IsibaniClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsibaniClient.Repository
{
    public class ClientProductRepository : IClientProductRepository
    {
        private readonly ClientDbContext db;

        public ClientProductRepository(ClientDbContext _db)
        {
            this.db = _db;
        }
        public  IEnumerable<ClientProductViewModel> GetAll()
        {
            var userVM = db.ClientProducts.Select(user => new ClientProductViewModel
            {Id=user.ClientId,
                ClientProducId = user.ClientProducId,
                ClientId = user.ClientId,

                ClientName = user.Client.Name,
                Address = user.Client.Address,
                ProductId = user.ProductId,
                ProductName = user.Product.Name,
                Type = user.Product.Type,
                Version = user.Product.Version,
                Description = user.Product.Description
            }).ToList();
            return userVM;
        }

        public ClientDetailsViewModel GetDetails(int ClientProducId = 0)
        {
            var userVM = db.ClientProducts.Where(x => x.ClientProducId == ClientProducId).Select(user => new ClientDetailsViewModel
            {
                //ClientProducts = user.Client.ClientProducts.ToList(),
                ClientId = user.Client.ClientId,
                 Name = user.Client.Name,
                Address = user.Client.Address,
                DateCreated = user.Client.DateCreated,
                DateLastEdited = user.Client.DateLastEdited,
                Deleted = user.Client.Deleted,
                UserId = user.Client.UserId,

            }).FirstOrDefault();
            return userVM;
        }
    }
}
