using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İnsanKaynaklariBilgi
{
    public class İkiliAramaAgaci
    {
        public List<Kisi> ikiYilUstu = new List<Kisi>();
        private IkiliAramaAgacDugumu kok;
        private string dugumler;
        public İkiliAramaAgaci()
        {

        }
        public İkiliAramaAgaci(IkiliAramaAgacDugumu kok)
        {
            this.kok = kok;
        }
        public int DugumSayisi()
        {
            return DugumSayisi(kok);
        }
        public int DugumSayisi(IkiliAramaAgacDugumu dugum)
        {
            int count = 0;
            if (dugum != null)
            {
                count = 1;
                count += DugumSayisi(dugum.sol);
                count += DugumSayisi(dugum.sag);
            }
            return count;
        }
        public int YaprakSayisi()
        {
            return YaprakSayisi(kok);
        }
        public int YaprakSayisi(IkiliAramaAgacDugumu dugum)
        {
            int count = 0;
            if (dugum != null)
            {
                if ((dugum.sol == null) && (dugum.sag == null))
                    count = 1;
                else
                    count = count + YaprakSayisi(dugum.sol) + YaprakSayisi(dugum.sag);
            }
            return count;
        }
        public string DugumleriYazdir()
        {
            return dugumler;
        }
        public void PreOrder()
        {
            dugumler = "";
            PreOrderInt(kok);
        }
        private void PreOrderInt(IkiliAramaAgacDugumu dugum)
        {
            if (dugum == null)
                return;
            Ziyaret(dugum);
            PreOrderInt(dugum.sol);
            PreOrderInt(dugum.sag);
        }
        public void InOrder()
        {
            dugumler = "";
            InOrderInt(kok);
        }
        private void InOrderInt(IkiliAramaAgacDugumu dugum)
        {
            if (dugum == null)
                return;
            InOrderInt(dugum.sol);
            Ziyaret(dugum);
            InOrderInt(dugum.sag);
        }
        private void Ziyaret(IkiliAramaAgacDugumu dugum)
        {
            dugumler += dugum.veri + " ";
        }
        public void PostOrder()
        {
            dugumler = "";
            PostOrderInt(kok);
        }
        private void PostOrderInt(IkiliAramaAgacDugumu dugum)
        {
            if (dugum == null)
                return;
            PostOrderInt(dugum.sol);
            PostOrderInt(dugum.sag);
            Ziyaret(dugum);
        }
        public void Ekle(ulong deger)
        {
            IkiliAramaAgacDugumu tempParent = new IkiliAramaAgacDugumu();
            IkiliAramaAgacDugumu tempSearch = kok;

            while (tempSearch != null)
            {
                tempParent = tempSearch;
                //Deger zaten var, çık.
                if (deger == (ulong)tempSearch.veri)
                    return;
                else if (deger < (ulong)tempSearch.veri)
                    tempSearch = tempSearch.sol;
                else
                    tempSearch = tempSearch.sag;
            }
            IkiliAramaAgacDugumu eklenecek = new IkiliAramaAgacDugumu(deger);
            if (kok == null)
                kok = eklenecek;
            else if (deger < (ulong)tempParent.veri)
                tempParent.sol = eklenecek;
            else
                tempParent.sag = eklenecek;
        }
        public IkiliAramaAgacDugumu Ara(ulong anahtar)
        {
            return AraInt(kok, anahtar);
        }
        private IkiliAramaAgacDugumu AraInt(IkiliAramaAgacDugumu dugum, ulong anahtar)
        {
            if (dugum == null)
                return null;
            else if ((ulong)dugum.veri == anahtar)
                return dugum;
            else if ((ulong)dugum.veri > anahtar)
                return (AraInt(dugum.sol, anahtar));
            else
                return (AraInt(dugum.sag, anahtar));
        }

        public IkiliAramaAgacDugumu MinDeger()
        {
            IkiliAramaAgacDugumu tempSol = kok;
            while (tempSol.sol != null)
                tempSol = tempSol.sol;
            return tempSol;
        }

        public IkiliAramaAgacDugumu MaksDeger()
        {
            IkiliAramaAgacDugumu tempSag = kok;
            while (tempSag.sag != null)
                tempSag = tempSag.sag;
            return tempSag;
        }

        private IkiliAramaAgacDugumu Successor(IkiliAramaAgacDugumu silDugum)
        {
            IkiliAramaAgacDugumu successorParent = silDugum;
            IkiliAramaAgacDugumu successor = silDugum;
            IkiliAramaAgacDugumu current = silDugum.sag;
            while (current != null)
            {
                successorParent = current;
                successor = current;
                current = current.sol;
            }
            if (successor != silDugum.sag)
            {
                successorParent.sol = successor.sag;
                successor.sag = silDugum.sag;

            }
            return successor;
        }

        public bool Sil(ulong deger)
        {
            IkiliAramaAgacDugumu current = kok;
            IkiliAramaAgacDugumu parent = kok;
            bool issol = true;
            while ((ulong)current.veri != deger)
            {
                parent = current;
                if (deger < (ulong)current.veri)
                {
                    issol = true;
                    current = current.sol;
                }
                else
                {
                    issol = false;
                    current = current.sag;
                }
                if (current == null)
                    return false;
            }
            if (current.sol == null && current.sag == null)
            {
                if (current == kok)
                {
                    kok = null;
                }
                else if (issol)
                {
                    parent.sol = null;
                }
                else
                {
                    parent.sag = null;
                }
            }
            else if (current.sag == null)
            {
                if (current == kok)
                {
                    kok = current.sol;
                }
                else if (issol)
                {
                    parent.sol = current.sol;
                }
                else
                {
                    parent.sag = current.sol;
                }
            }
            else if (current.sol == null)
            {
                if (current == kok)
                {
                    kok = current.sag;
                }
                else if (issol)
                {
                    parent.sol = current.sag;
                }
                else
                {
                    parent.sag = current.sag;
                }
            }
            else
            {
                IkiliAramaAgacDugumu successor = Successor(current);
                if (current == kok)
                    kok = successor;
                else if (issol)
                    parent.sol = successor;
                else
                    parent.sag = successor;
                successor.sol = current.sol;
            }
            return true;
        }
        public IkiliAramaAgacDugumu YabanciDilAra(string anahtar)
        {
            return YabanciDilAraInt(kok, anahtar);
        }
        private IkiliAramaAgacDugumu YabanciDilAraInt(IkiliAramaAgacDugumu dugum, string anahtar)
        {
            if (dugum == null)
                return null;
            else if (string.Compare( (string)dugum.veri,anahtar)==0)
                return dugum;
            else if (string.Compare((string)dugum.veri,anahtar)<0)
                return (YabanciDilAraInt(dugum.sol, anahtar));
            else
                return (YabanciDilAraInt(dugum.sag, anahtar));
        }
        public int ElemanSayisi()
        {
            return DugumSayisi(kok);
        }
        public int DerinlikHesapla(IkiliAramaAgacDugumu dugum)
        {
            int yukseklik_sag = 0, yukselik_sol = 0;
            if (dugum != null)
            {
                yukseklik_sag = DerinlikHesapla(dugum.sag);
                yukselik_sol = DerinlikHesapla(dugum.sol);
                if (yukseklik_sag > yukselik_sol)
                {
                    return yukseklik_sag + 1;
                }
                else
                {
                    return yukselik_sol + 1;
                }
            }
            else
            {
                return 0;
            }
        }
        public int AgacDerinlikSayisi()
        {
            return DerinlikHesapla(kok);
        }

    }
}
    



