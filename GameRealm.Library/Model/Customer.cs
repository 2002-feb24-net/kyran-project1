using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameRealm.Domain.Model
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
        }

        public int CustomerId { get; set; }
        [Display(Name = "First")]
        public string FirstName { get; set; }
        [Display(Name = "Last")]
        public string LastName { get; set; }
        public string Phone { get; set; }
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Password { get; set; }
        [Display(Name = "Profile Name")]
        public string UserName { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
