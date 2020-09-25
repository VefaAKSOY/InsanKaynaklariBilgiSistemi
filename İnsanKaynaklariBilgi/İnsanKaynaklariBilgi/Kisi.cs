using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İnsanKaynaklariBilgi
{
    public class Kisi       
    {

        public KimlikBilgileri KimlikBilgileri { get; set; }
        public string Adres { get; set; }
        public string TelNo { get; set; }
        public string YabanciDil { get; set; }
        public string IlgiAlanlari { get; set; }
        public string Referans { get; set; }
  
        public int MusteriNo { get; set; }
        public string Eposta { get; set; }
        public int Deneyim { get; set; }



        private LinkedList IsDeneyimiListesi;
        private LinkedList EgitimDurumuListesi;
       

        public Kisi()
        {
            this.IsDeneyimiListesi = new LinkedList();
            this.EgitimDurumuListesi = new LinkedList();
          
            KimlikBilgileri = new KimlikBilgileri();
        }
        public void IsDeneyimiEkle(IsDeneyimi isDeneyimi)
        {
            IsDeneyimiListesi.InsertLast(isDeneyimi);
           
        }
        public void EgitimDurumuEkle(EgitimDurumu egitimDurumu)
        {
            EgitimDurumuListesi.InsertLast(egitimDurumu);
        }

        public IsDeneyimi GetIsDeneyimi(int position)
        {
            return (IsDeneyimi)IsDeneyimiListesi.GetElement(position).Data;
        }
        public EgitimDurumu GetEgitimDurumu(int position)
        {
            return (EgitimDurumu)EgitimDurumuListesi.GetElement(position).Data;
        }
    }
}
