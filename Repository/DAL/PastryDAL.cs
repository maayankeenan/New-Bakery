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
        public static void AddNewPastry(Pastery NewPastry)
        {
            using (PastryContext db = new PastryContext())
            {

                db.Pastries.Add(NewPastry);
                db.SaveChanges();
            }
        }

        public static List<Pastery> GetAllPastries()
        {
            using (PastryContext db = new PastryContext())
            {
                return db.Pastries.ToList();
            }
        }

        

        public static void UpdatePastry(Pastery PastryToUpdate)
        {
            using (var db = new PastryContext())
            {
                db.Pastries.AddOrUpdate(PastryToUpdate);
                int num = db.SaveChanges();
            }
        }

        public static Pastery GetPatry(Pastery pastry)
        {
            using (PastryContext db = new PastryContext())
            {
                return (from past in db.Pastries
                        where past.ID == pastry.ID
                        select past).FirstOrDefault();
            }
        }

        public static void DeletePastry(Pastery PastryToRemove)
        {
            using (PastryContext db = new PastryContext())
            {
                var pastry = GetPatry(PastryToRemove);

                db.Entry(pastry).State = EntityState.Deleted;

                int num = db.SaveChanges();
            }
        }

        public static List<Pastery> SelectByCriteria (int? pasID=null, string pasName=null, PastryType? pasType=null, Double? pasPrice=null, string pasComments=null, bool? pasVegan=null, bool? pasGlotanFree=null)
        {
            using (PastryContext db = new PastryContext())
            {
                IList<Pastery> result = db.Pastries.ToList();

                if (pasID != null)
                {
                    result = result.Where(p => p.ID.Equals(pasID)).ToList();
                }
                if (pasName != null)
                {
                    result = result.Where(p => p.Name.ToLower().Contains(pasName.ToLower())).ToList();
                }
                if (pasType != null)
                {
                    result = result.Where(p => p.Type==pasType).ToList();
                }
                if (pasPrice != null)
                {
                    result = result.Where(p => p.Price == pasPrice).ToList();
                }
                if (pasComments != null)
                {
                    result = result.Where(p => p.Comments.Contains(pasComments)).ToList();
                }
                if (pasVegan != null)
                {
                    result = result.Where(p => p.Vegan==pasVegan).ToList();
                }
                if (pasGlotanFree != null)
                {
                    result = result.Where(p => p.GlotanFree == pasGlotanFree).ToList();
                }

                return result.ToList();
                
            }
        }

        public byte[] GetPhoto(string filePath)
        {
            FileStream stream = new FileStream(
                filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        }
    }
}
