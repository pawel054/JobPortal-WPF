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
        public Company Company { get; set; }
        public Category Category { get; set; }
        public string NazwaStanowiska { get; set; }
        public string PoziomStanowiska { get; set; }
        public string RodzajUmowy { get; set; }
        public string RodzajPracy { get; set; }
        public string WymiarZatrudnienia { get; set; }
        public string Wynagrodzenie { get; set; }
        public string DniPracy { get; set; }
        public string GodzinyPracy { get; set; }
        public DateTime DataWaznosci { get; set; }
        public string SciezkaObraz { get; set; }

        public Offer(int offerID, Company company, Category category, string nazwaStanowiska, string poziomStanowiska, string rodzajUmowy, string rodzajPracy, string wymiarZatrudnienia, string wynagrodzenie, string dniPracy, string godzinyPracy, DateTime dataWaznosci, string sciezkaObraz)
        {
            OfferID = offerID;
            Company = company;
            Category = category;
            NazwaStanowiska = nazwaStanowiska;
            PoziomStanowiska = poziomStanowiska;
            RodzajUmowy = rodzajUmowy;
            RodzajPracy = rodzajPracy;
            WymiarZatrudnienia = wymiarZatrudnienia;
            Wynagrodzenie = wynagrodzenie;
            DniPracy = dniPracy;
            GodzinyPracy = godzinyPracy;
            DataWaznosci = dataWaznosci;
            SciezkaObraz = sciezkaObraz;
        }
    }
}
