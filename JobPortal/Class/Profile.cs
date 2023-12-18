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
        public string Adress { get; set; }
        public string WorkPosition { get; set; }
        public string WorkPositionDescription { get; set; }
        public string CareerSummary { get; set; }

        public Profile(int profileID, int userID, string name, string surname, DateTime birthDate, string phoneNumber, string profilePictureSrc, string adress, string workPosition, string workPositionDescription, string careerSummary)
        {
            ProfileID = profileID;
            UserID = userID;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            ProfilePictureSrc = profilePictureSrc;
            Adress = adress;
            WorkPosition = workPosition;
            WorkPositionDescription = workPositionDescription;
            CareerSummary = careerSummary;
        }

        public Profile(int profileID, string name, string surname, DateTime birthDate, string phoneNumber, string profilePictureSrc, string adress, string workPosition, string workPositionDescription, string careerSummary)
        {
            ProfileID = profileID;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            ProfilePictureSrc = profilePictureSrc;
            Adress = adress;
            WorkPosition = workPosition;
            WorkPositionDescription = workPositionDescription;
            CareerSummary = careerSummary;
        }

        public Profile( string name, string surname, DateTime birthDate, string phoneNumber, string profilePictureSrc, string adress, string workPosition, string workPositionDescription, string careerSummary)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            ProfilePictureSrc = profilePictureSrc;
            Adress = adress;
            WorkPosition = workPosition;
            WorkPositionDescription = workPositionDescription;
            CareerSummary = careerSummary;
        }
    }
}
