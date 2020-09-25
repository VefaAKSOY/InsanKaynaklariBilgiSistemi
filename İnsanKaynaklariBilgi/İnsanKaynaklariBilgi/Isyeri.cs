using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İnsanKaynaklariBilgi
{
    public class Isyeri
    {
        public string IsyeriAdi { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Faks { get; set; }
        public string Eposta { get; set; }
        public int IsyeriNo { get; set; }
        
        IsIlani isilani = new IsIlani();
        public IsIlani IlanVer()
        {
            return isilani;
        }
    }
}
