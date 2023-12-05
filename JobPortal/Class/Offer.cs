using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Class
{
    public class Offer
    {
        public int OfferID { get; set; }
        public int CompanyID { get; set; }
        public int CategoryID { get; set; }
        public string PositionName { get; set; }
        public string PositionLevel { get; set; }
        public string ContractTyoe { get; set; }
        public string JobType { get; set; }
        public string Salary { get; set; }
        public string WorkingDays { get; set; }
        public string WorkingHours { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CompanyInformation { get; set; }

    }
}
