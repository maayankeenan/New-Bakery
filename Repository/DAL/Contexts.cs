using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public static class Contexts
    {
        static PastryContext db = null;

        public static PastryContext DB
        {
            get
            {
                if (db == null)
                {
                    db = new PastryContext();
                    //PastryDAL.initData();
                }
                return db;
            }
        }
    }
}
