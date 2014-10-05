using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Repository.DAL
{
    public class CustomerDAL
    {
        public void AddNewCustomer(Customer NewCustomer)
        {
            using (PastryContext db = new PastryContext())
            {

                db.Customers.Add(NewCustomer);
                db.SaveChanges();

            }
        }

        public List<Customer> GetAllCustomers()
        {
            using (PastryContext db = new PastryContext())
            {
                return db.Customers.ToList();
            }

        }

        public void UpdateCustomer(Customer customerToUpdate)
        {
            using (var db = new PastryContext())
            {
                db.Customers.AddOrUpdate(customerToUpdate);
                int num = db.SaveChanges();
            }
        }

        public Customer GetCustomer(string ID)
        {
            using (PastryContext db = new PastryContext())
            {
                return (from cust in db.Customers
                        where cust.ID == ID
                        select cust).FirstOrDefault();
            }
        }

        public void DeleteCustomer(string customerIDToRemove)
        {
            using (PastryContext db = new PastryContext())
            {
                var customer = GetCustomer(customerIDToRemove);
                db.Entry(customer).State = EntityState.Deleted;

                int num = db.SaveChanges();
            }
        }

        public List<Customer> SelectByCriteria(string id = null, string name = null, string lastName = null, string address = null, string phone = null, string password = null, bool? isadmin = null, bool? prefered = null)
        {
            using (PastryContext db = new PastryContext())
            {
                IList<Customer> result = db.Customers.ToList();

                if (id != null)
                {
                    result = result.Where(p => p.ID.Equals(id)).ToList();
                }
                if (name != null)
                {
                    result = result.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
                }
                if (lastName != null)
                {
                    result = result.Where(p => p.LastName.ToLower().Contains(lastName.ToLower())).ToList();
                }
                if (address != null)
                {
                    result = result.Where(p => p.Address.ToLower().Contains(address.ToLower())).ToList();
                }
                if (phone != null)
                {
                    result = result.Where(p => p.Phone.Equals(phone)).ToList();
                }
                if (password != null)
                {
                    result = result.Where(p => p.Password.Equals(password)).ToList();
                }
                if (prefered != null)
                {
                    result = result.Where(p => p.Prefered == prefered).ToList();
                }
                if (isadmin != null)
                {
                    result = result.Where(p => p.IsAdmin == isadmin).ToList();
                }

                return result.ToList();
            }
        }

    }
}
