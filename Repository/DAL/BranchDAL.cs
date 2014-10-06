using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class BranchDAL
    {
        public void initData()
        {
            var Branches = new List<Branch>
                {
                    new Branch {ID=1, Name="Ramat-Gan - Zabutinsky", Phone = "03-845739", X = 32.084609, Y = 34.809756},
                    new Branch {ID=2, Name="Tel-Aviv Hamedina square", Phone = "03-842579", X = 32.085955, Y = 34.788577},
                    new Branch {ID=3, Name="Tel-Aviv Rabin square", Phone = "03-3643679", X = 32.080555, Y = 34.781432},
                };

            foreach (var branch in Branches)
            {
                //AddNewBranch(branch);
            }
        }

        public static List<Branch> allBranches()
        {
            using (PastryContext db = new PastryContext())
            {
                return db.Branches.ToList();
            }
        }
    }
}
