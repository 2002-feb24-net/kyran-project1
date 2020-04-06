using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameRealmWeb.ViewModels
{
    public class CustomersViewModel
    {
        
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "You must enter your First Name!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "You must enter your Last Name!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "You must enter your Phone Number!")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "You must enter your E-Mail!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "You must enter a Password!")]
        [MaxLength(20)]
        public string Password { get; set; }
        [Required(ErrorMessage = "You must enter a Username!")]
        [MaxLength(50)]
        public string UserName { get; set; }
    }
}
