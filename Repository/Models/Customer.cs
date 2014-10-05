using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Customer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Prefered { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Order> allRelatedOrders { get; set; }

        public Customer()
        {
        }

        public Customer(string custID, string custFirstName, string custLastName, string custAddress, string custPhone, bool custPrefered, string cusrPassword,bool bIsAdmin)
        {
            this.ID = custID;
            this.Name = custFirstName;
            this.LastName = custLastName;
            this.Address = custAddress;
            this.Phone = custPhone;
            this.Prefered = custPrefered;
            this.Password = cusrPassword;
            this.IsAdmin = bIsAdmin;
        }

        
    }
}
