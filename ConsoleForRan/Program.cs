using Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Repository.Models;
using Repository.Enum;

namespace ConsoleForRan
{
    class Program
    {
        static void Main(string[] args)
        {
                        using (var context = new PastryContext())
            {
              /*
                var Pastries = new List<Pastry>
                {
                    new Pastry { ID = 0,   Name = "בורקס חלומי", 
                        Type = PastryType.Burekas ,Vegan=false, glotanFree=false  },
                    new Pastry { ID = 1,   Name = "לחם אלוהי",
                        Type = PastryType.Bread ,Vegan=false, glotanFree=false  },
                    new Pastry { ID = 2,   Name = "בורקס אלוהי",
                        Type = PastryType.Bread ,Vegan=false, glotanFree=false  },
                
                };

                    Pastries.ForEach(s => context.Pastries.AddOrUpdate(p => p.ID, s));
                    context.SaveChanges();

                    var Customers = new List<Customer>
                {
                    new Customer {ID="305631491", Name="Ran", LastName="Asulin", Address="Amishav 66 Tel Aviv", Phone="050-3233758", Prefered=true, Password="123", IsAdmin=false},
                    new Customer {ID="305631490", Name="May", LastName="Asulin", Address="Amishav 66 Tel Aviv", Phone="050-3233758", Prefered=true, Password="123", IsAdmin=true},
                };

                    Customers.ForEach(s => context.Customers.AddOrUpdate(p => p.ID, s));
                    context.SaveChanges();

                    var Orders = new List<Order>
                {
                    new Order { OrderID="1", OrderDate=DateTime.Today.Date, DeliveryDate=DateTime.Today.Date.AddDays(1), Status=OrderStatus.Accepted ,CustomerID="305631491"},
                };

                    Orders.ForEach(s => context.Orders.AddOrUpdate(p => p.OrderID, s));
                    context.SaveChanges();

                
                   

                    var OrdersDetailes = new List<OrderDetails>
                {
                    new OrderDetails {ID="0", OrderID="1", pastryID= 2, TotalAmount=2},
                };

                    OrdersDetailes.ForEach(s => context.OrderDetailes.AddOrUpdate(p => p.ID, s));
                    context.SaveChanges();

                    var Branches = new List<Branch>
                {
                    new Branch {ID=0, Name="Ran's Pastry", X=5.0, Y=3.0},
                    new Branch {ID=1, Name="Rani's Pastry", X=5.0, Y=3.0},
                };

                    Branches.ForEach(s => context.Branches.AddOrUpdate(p => p.ID, s));
                    context.SaveChanges();
                */
                
                //CustomerDAL f = new CustomerDAL();
                //f.AddNewCustomer(new Customer("123789456","Ido","Ganzer","lalala 12", "035-9856985",true,"Aa",false));
                //f.DeleteCustomer(new Customer("123789456", "Ido", "Ganzer", "Bialik 5", "036-9854745", true, "SD", false));
                //var ran = f.GetCustomer(new Customer("305631491","Ran", "Asulin", "Amishav 66 Tel Aviv", "050-3233758", true, "123", false));
                //f.GetAllCustomers();
                //f.SelectByCriteria(null, null, null,null,null,null,true);
                //f.SelectByCriteria("305631491");
                //f.SelectByCriteria(null, null, "Asulin", null, "050-3233758");


                //PastryDAL p = new PastryDAL();

                //p.AddNewPastry(new Pastry(5, "קוראסון אוכמניות", p.GetPhoto(@"C:\Users\ran\Source\Repos\Backery\ConsoleForRan\Images\untitled.png"), PastryType.Croissant, 12, string.Empty, false, false));
                //p.UpdatePastry(new Pastry(2, "בורקס טורקי", null, PastryType.Burekas, 13, string.Empty, false, false));
                //p.DeletePastry(new Pastry(2, "בורקס טורקי", null, PastryType.Burekas, 13, string.Empty, false, false));
                //p.SelectByCriteria(null, "בורקס");
                //p.SelectByCriteria(null, null, PastryType.Burekas);
                //p.SelectByCriteria(null, null, PastryType.Burekas,null,null,true);

                OrderdDAL o = new OrderdDAL();
                //o.AddNewOrder(new Order("1234", DateTime.Today, DateTime.Today.Date.AddDays(1), 0, string.Empty, OrderStatus.InProgress, "305631491"));
                //o.UpdateOrder(new Order("1212", DateTime.Today.Date, DateTime.Today.Date.AddDays(1), 0, "Amazing", OrderStatus.InProgress, "305631491"));
                //o.DeleteOrder(new Order(1212, DateTime.Today.Date, DateTime.Today.Date.AddDays(1), 0, "Amazing", OrderStatus.InProgress, "305631491"));
                //o.GetAllOrders();
                //o.SelectByCriteria("1");
                //o.SelectByCriteria(null, null, null, null, null, OrderStatus.Accepted);

            }
        }
    }
    
}
