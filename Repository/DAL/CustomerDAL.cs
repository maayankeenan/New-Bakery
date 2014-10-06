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
        public void initData()
        {
            var Customers = new List<Customer>
                {
                    new Customer {ID="305631491", Name="Ran", LastName="Asulin", Address="Amishav 66 Givataim", Phone="050-3233758", Prefered=true, Password="123", IsAdmin=false},
                    new Customer {ID="304848370", Name="May", LastName="Cohen", Address="Arlozorov 44 Tel Aviv", Phone="050-3267858", Prefered=true, Password="123456", IsAdmin=true},
                    new Customer {ID="367867567", Name="Amikam", LastName="Snir", Address="Arlozorov 56 Tel Aviv", Phone="050-4389275", Prefered=true, Password="654321", IsAdmin=false},
                    new Customer {ID="345674567", Name="Nir", LastName="Agai", Address="Dr cohen 13 Ramat Gan", Phone="050-4246857", Prefered=false, Password="123456", IsAdmin=false},
                    new Customer {ID="456734564", Name="Blinki", LastName="Bill", Address="Hertzel 9 Naharia", Phone="050-3267858", Prefered=true, Password="76543", IsAdmin=false},
                    new Customer {ID="345765687", Name="Amit", LastName="Mizrahi", Address="Beit horon 2 Ramat Gan", Phone="050-3267858", Prefered=false, Password="nbuv48", IsAdmin=false},
                    new Customer {ID="456788978", Name="Tikva", LastName="Shasson", Address="Some street 33 Beer sheva", Phone="050-3267858", Prefered=true, Password="gt67gt678", IsAdmin=false},
                    new Customer {ID="456786879", Name="Marina", LastName="Abutbul", Address="Nordau 21 Netanya", Phone="050-3267858", Prefered=false, Password="Aa123456", IsAdmin=false},
                    new Customer {ID="657586747", Name="Eran", LastName="Levi", Address="Best street ever 5 Tel Aviv", Phone="050-3267858", Prefered=true, Password="tututtu", IsAdmin=false},
                    new Customer {ID="567857898", Name="Maria", LastName="Sharapova", Address="Dizingoff 46 Tel Aviv", Phone="050-3267858", Prefered=false, Password="blaaa", IsAdmin=false},
                    new Customer {ID="456789786", Name="Lebron", LastName="James", Address="Hamahapilim 8 Netanya", Phone="050-3267858", Prefered=true, Password="mypassword", IsAdmin=false},
                };

            foreach(var cust in Customers)
            {
                AddNewCustomer(cust);
            }
        }

        public static void AddNewCustomer(Customer NewCustomer)
        {
            using (PastryContext db = new PastryContext())
            {

                db.Customers.Add(NewCustomer);
                db.SaveChanges();

            }
        }

        public static List<Customer> GetAllCustomers()
        {
            using (PastryContext db = new PastryContext())
            {
                return db.Customers.ToList();
            }

        }

        public static void UpdateCustomer(Customer customerToUpdate)
        {
            using (var db = new PastryContext())
            {
                db.Customers.AddOrUpdate(customerToUpdate);
                int num = db.SaveChanges();
            }
        }

        public static Customer GetCustomer(string ID)
        {
            using (PastryContext db = new PastryContext())
            {
                return (from cust in db.Customers
                        where cust.ID == ID
                        select cust).FirstOrDefault();
            }
        }

        public static void DeleteCustomer(string customerIDToRemove)
        {
            using (PastryContext db = new PastryContext())
            {
                var customer = GetCustomer(customerIDToRemove);
                db.Entry(customer).State = EntityState.Deleted;

                int num = db.SaveChanges();
            }
        }

        public static List<Customer> SelectByCriteria(string id = null, string name = null, string lastName = null, string address = null, string phone = null, string password = null, bool? isadmin = null, bool? prefered = null)
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
