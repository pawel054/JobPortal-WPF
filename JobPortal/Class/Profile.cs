using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Class
{
    public class Profile
    {
        public int ProfileID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureSrc { get; set; }
        public string Residence { get; set; }
        public string WorkPosition { get; set; }
        public string WorkPositionDescription { get; set; }
        public string CareerSummary { get; set; }
    }
}
