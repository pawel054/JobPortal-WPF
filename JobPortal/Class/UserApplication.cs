using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Class
{
    public class UserApplication
    {
        public int ID { get; set; }
        public User user { get; set; }
        public Offer offer { get; set; }
        public string Status {  get; set; }

        public UserApplication(int iD, User user, Offer offer, string status)
        {
            ID = iD;
            this.user = user;
            this.offer = offer;
            Status = status;
        }

        public UserApplication(User user, Offer offer, string status)
        {
            this.user = user;
            this.offer = offer;
            Status = status;
        }
    }
}
