using Repository.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Pastery
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public PastryType Type { get; set; }
        public Double Price { get; set; }
        public string Comments { get; set; }
        public bool Vegan { get; set; }
        public bool GlotanFree { get; set; }

        public virtual ICollection<OrderDetails> OrdersDetailes { get; set; }

        public Pastery()
        {
        }

        public Pastery(int pasID, string pasName, string pasPicture, PastryType pasType, float pasPrice, string pasComments, bool pasVegan, bool pasGlotanFree)
        {                  
            ID = pasID;
            Name = pasName;
            ImageLink = pasPicture;
            Type = pasType;
            Price =pasPrice;
            Comments = pasComments;
            Vegan = pasVegan;
            GlotanFree = pasGlotanFree;
        }
    }
}
