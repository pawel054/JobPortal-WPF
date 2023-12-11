using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Class
{
    public class OfferTables
    {
        public int ID {  get; set; }
        public string Content { get; set; }
        public int OfferID {  get; set; }

        public OfferTables(int iD, string content, int offerID)
        {
            ID = iD;
            Content = content;
            OfferID = offerID;
        }
    }
}
