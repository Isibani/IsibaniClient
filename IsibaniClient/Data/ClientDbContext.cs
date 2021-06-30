
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace IsibaniClient.Data
{





    //public class MyObject
    //{
    //    public int MyObjectId { get; set; }

    //    public string MyObjectName { get; set; }

    //    // other properties

    //    public virtual ApplicationUser ApplicationUser { get; set; }
    //}

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    public DbSet<MyObject> MyObjects { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);

    //        modelBuilder.Entity<MyObject>()
    //            .HasRequired(c => c.ApplicationUser)
    //            .WithMany(t => t.MyObjects)
    //            .Map(m => m.MapKey("UserId"));
    //    }
    //}


    public class ClientDbContext : IdentityDbContext<IdentityUser>
    {
        public ClientDbContext()
        {
        }

        public ClientDbContext(DbContextOptions options) : base(options)
        {
        }
       
      
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ClientProduct> ClientProducts { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
   
}
