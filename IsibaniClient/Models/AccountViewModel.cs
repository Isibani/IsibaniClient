using IsibaniClient.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsibaniClient.Models
{
   
    public class RegisterViewModel
    {
        public string Id { get; set; }
        public int ClientId { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ProductViewModel
    {
        public int? ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Version { get; set; }
        [Required]
        public string Description { get; set; }
    }
    public class ClientProductViewModel
    {
        public int ClientProducId { get; set; }
        public int ClientId { get; set; }
        public int Id { get; set; }

        public string ClientName { get; set; }
        public string Address { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public int Estimatedbudget { get; set; }
        public int Amountspent { get; set; }
        public int Duration { get; set; }



    }
}
