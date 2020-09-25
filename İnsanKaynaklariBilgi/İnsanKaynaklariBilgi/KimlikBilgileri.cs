using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İnsanKaynaklariBilgi
{
    public class KimlikBilgileri
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Uyruk { get; set; }
        public string DogumYeri { get; set; }
        public string DogumTarihi { get; set; }
        public MedeniDurum MedeniDurum { get; set; }
        
        private ulong TCKimlikNo;

        public ulong TcKimlikNo
        {
            get
            {
                return TCKimlikNo;
            }
            set
            {
                TCKimlikNo = value;
            }
        }
    }
}
