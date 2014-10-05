﻿using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class BranchDAL
    {
        public List<Branch> allBranches()
        {
            using (PastryContext db = new PastryContext())
            {
                return db.Branches.ToList();
            }
        }
    }
}