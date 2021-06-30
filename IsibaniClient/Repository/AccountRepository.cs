using IsibaniClient.Data;
using IsibaniClient.IRepository;
using IsibaniClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsibaniClient.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly ClientDbContext db;

        public AccountRepository(ClientDbContext _db)
        {
            this.db = _db;
        }
        public IEnumerable<RegisterViewModel> GetAll()
        {
            var userVM = db.Clients.Select(user => new RegisterViewModel
            {
                Id = user.IdentityUser.Id,
                Name = user.Name,
                Email = user.IdentityUser.Email,
                Address = user.Address,
                ClientId = user.ClientId,
                PhoneNumber = user.IdentityUser.PhoneNumber
            }).ToList();
            return userVM;
        }
    }
}
