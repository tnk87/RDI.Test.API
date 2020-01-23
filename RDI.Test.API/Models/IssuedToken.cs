using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDI.Test.API.Models
{
    public class IssuedToken
    {
        [Key]
        public string Token { get; set; }
        public DateTime RegistrationDate { get; set; }

        public long CardNumber { get; set; }
        public int CVV { get; set; }
    }
}
