﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Class
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }

        public Company(int companyID, string name, string adress)
        {
            CompanyID = companyID;
            Name = name;
            Adress = adress;
        }
    }
}
