using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using Repository.Enum;

namespace Repository.DAL
{
    public class PastryDAL
    {
        static PastryContext db = new PastryContext();

        public static void initData()
        {
            List<Pastery> pasteries = new List<Pastery>();
            pasteries.Add(new Pastery() { Name = "Choclate Cake", Type = PastryType.Cakes, GlotanFree = false, Vegan = false, Comments = "Very Delicious", Price = 9.99, ImageLink = "http://www.sparkyhub.com/wp-content/uploads/2012/02/30+delicious-chocolate-cake-pictures-you-love-4.jpg?0c9e3b" });
            pasteries.Add(new Pastery() { Name = "Choclate Strewberry Cupcake", Type = PastryType.Cupcakes, GlotanFree = false, Vegan = false, Comments = "Fresh Strewberry", Price = 5.49, ImageLink = "http://www.localfranchiseopportunities.net/images/food-franchise-opportunities/food-franchise-opportunities-wyoming.jpg" });
            pasteries.Add(new Pastery() { Name = "Rye Bread", Type = PastryType.Breads, GlotanFree = false, Vegan = false, Price = 14.90, ImageLink = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcQDtD8t2IEb5veW4YmdjByl5NXU3WEyMlyrP01IU0MNrDgxnklL" });
            pasteries.Add(new Pastery() { Name = "Onion Bagel", Type = PastryType.Bagels, GlotanFree = false, Vegan = false, Comments = "Dry onion", Price = 3.49, ImageLink = "http://featherstonefoods.com/wp-content/uploads/1108.jpg" });
            pasteries.Add(new Pastery() { Name = "Red Velvet Cupcake", Type = PastryType.Cupcakes, GlotanFree = false, Vegan = false, Comments = "Our signture cupcake", Price = 4.99, ImageLink = "http://images4.fanpop.com/image/photos/15400000/red-velvet-cupcakes-red-velvet-cupcakes-15404907-460-562.jpg" });
            pasteries.Add(new Pastery() { Name = "Apple Pie", Type = PastryType.Cakes, GlotanFree = true, Vegan = false, Comments = "The traditional taste", Price = 24.90, ImageLink = "http://img2.wikia.nocookie.net/__cb20130114141601/shipoffools/images/f/f7/Apple-pie.jpg" });
            pasteries.Add(new Pastery() { Name = "Oreo Cupcake", Type = PastryType.Cupcakes, GlotanFree = false, Vegan = false, Price = 4.99, ImageLink = "http://1.bp.blogspot.com/-v304FcYagNk/TbQgccwSHzI/AAAAAAAAAFw/nrd0qURLr-I/s1600/oreo+cupcake.jpg" });
            pasteries.Add(new Pastery() { Name = "Seaseme Bagel", Type = PastryType.Bagels, GlotanFree = false, Vegan = false, Price = 3.99, ImageLink = "http://www.gourmetmeatman.com/siteimages/sesame%20bagel.jpg" });
            pasteries.Add(new Pastery() { Name = "Olive Bread", Type = PastryType.Breads, GlotanFree = false, Vegan = false, Comments = "Calamate Olieves", Price = 19.99, ImageLink = "http://www.acebakery.com/storage/products/OliveBoule.jpg" });
            pasteries.Add(new Pastery() { Name = "Carrot Cake", Type = PastryType.Cakes, GlotanFree = true, Vegan = true, Price = 39.99, ImageLink = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcS6csPHFf1EpBTZ7wvXuRN35ejVeLfeEA0oqPbvP9lkv8Byt1-J" });
            pasteries.Add(new Pastery() { Name = "Vanilla Cupcake", Type = PastryType.Cupcakes, GlotanFree = false, Vegan = false, Price = 4.99, ImageLink = "http://images.picturesdepot.com/photo/v/vanilla_cupcake-210820.jpg" });
            pasteries.Add(new Pastery() { Name = "Whole Wheat Bread", Type = PastryType.Breads, GlotanFree = false, Vegan = false, Comments = "Very nutritious", Price = 29.99, ImageLink = "http://www.womansday.com/cm/womansday/images/IQ/What-to-Look-for-When-Buying-Bread-mdn.jpg" });

            foreach (var pastery in pasteries)
            {
                AddNewPastry(pastery);
            }
        }

        public static int AddNewPastry(Pastery NewPastry)
        {
            //using (PastryContext db = new PastryContext())
            //{
            Contexts.DB.Pastries.Add(NewPastry);
            Contexts.DB.SaveChanges();

            return NewPastry.ID;
            //}
        }

        public static List<Pastery> GetAllPastries()
        {
            //using (PastryContext Contexts.DB = new PastryContext())
            //{
            return Contexts.DB.Pastries.ToList();
            //}
        }



        public static void UpdatePastry(Pastery PastryToUpdate)
        {
            //using (var Contexts.DB = new PastryContext())
            {
                Contexts.DB.Pastries.AddOrUpdate(PastryToUpdate);
                int num = Contexts.DB.SaveChanges();
            }
        }

        public static Pastery GetPatry(int pastryID)
        {
            //using (PastryContext Contexts.DB = new PastryContext())
            {
                return (from past in Contexts.DB.Pastries
                        where past.ID == pastryID
                        select past).FirstOrDefault();
            }
        }

        public static void DeletePastry(Pastery PastryToRemove)
        {
            //using (PastryContext Contexts.DB = new PastryContext())
            {
                var pastry = GetPatry(PastryToRemove.ID);
                OrderdDAL.DeleteAllRelatedDetails(pastry.OrdersDetailes);
                Contexts.DB.Entry(pastry).State = EntityState.Deleted;

                int num = Contexts.DB.SaveChanges();
            }
        }

        public static List<Pastery> SelectByCriteria(int? pasID = null, string pasName = null, PastryType? pasType = null, Double? pasPrice = null, bool? pasVegan = null, bool? pasGlotanFree = null)
        {
            //using (PastryContext Contexts.DB = new PastryContext())
            {
                IList<Pastery> result = Contexts.DB.Pastries.ToList();

                if (pasID != null)
                {
                    result = result.Where(p => p.ID.Equals(pasID)).ToList();
                }
                if (pasName != "" && pasName != null)
                {
                    result = result.Where(p => p.Name.ToLower().Contains(pasName.ToLower())).ToList();
                }
                if (pasType != null)
                {
                    result = result.Where(p => p.Type == pasType).ToList();
                }
                if (pasPrice != null)
                {
                    result = result.Where(p => p.Price == pasPrice).ToList();
                }
                if (pasVegan != null)
                {
                    result = result.Where(p => p.Vegan == pasVegan).ToList();
                }
                if (pasGlotanFree != null)
                {
                    result = result.Where(p => p.GlotanFree == pasGlotanFree).ToList();
                }


                return result.ToList();

            }
        }
    }
}
