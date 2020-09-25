using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace İnsanKaynaklariBilgi
{
    public class HashIlanlar
    {
        int TABLE_SIZE = 100;

        HashChainEntry[] table;
        public HashIlanlar()
        {
            table = new HashChainEntry[TABLE_SIZE];
            for (int i = 0; i < TABLE_SIZE; i++)
                table[i] = null;
        }
        public void IlanEkle(int IlanNo, IsIlani IlanBilgileri)
        {
            int hash = (IlanNo % TABLE_SIZE);
            if (table[hash] == null)
                table[hash] = new HashChainEntry(IlanNo, IlanBilgileri);
            else
            {
                HashChainEntry entry = table[hash];
                while (entry.Next != null && entry.Anahtar != IlanNo)
                    entry = entry.Next;
                if (entry.Anahtar == IlanNo)
                    entry.Deger = IlanBilgileri;
                else
                    entry.Next = new HashChainEntry(IlanNo, IlanBilgileri);
            }
        }
        public IsIlani IsIlaniGetir(int IlanNo)
        {
            int hash = (IlanNo % TABLE_SIZE);
            if (table[hash] == null)
                return null;
            else
            {
                HashChainEntry entry = table[hash];
                while (entry != null && entry.Anahtar != IlanNo)
                    entry = entry.Next;
                if (entry == null)
                    return null;
                else
                    return (IsIlani)entry.Deger;
            }
        }

        public void IlanKaldır(int IlanNo)
        {
            int hash = (IlanNo % TABLE_SIZE);
            while (table[hash] != null && table[hash].Anahtar % TABLE_SIZE != IlanNo % TABLE_SIZE)
            {
                hash = (hash + 1) % TABLE_SIZE;
            }
            HashChainEntry current = table[hash];
            bool isRemoved = false;
            while (current != null)
            {
                if (current.Anahtar == IlanNo)
                {
                    table[hash] = current.Next;
                    isRemoved = true;
                    break;
                }

                if (current.Next != null)
                {
                    if (current.Next.Anahtar == IlanNo)
                    {
                        HashChainEntry newNext = current.Next.Next;
                        current.Next = newNext;
                        isRemoved = true;
                        break;
                    }
                    else
                        current = current.Next;
                }

            }

            if (!isRemoved)
            {
                MessageBox.Show("Silinecek bir şey bulunamadı!");
                return;
            }
            
        }
        public List<IsIlani> IlanListesi()
        {
            List<IsIlani> Ilanlar = new List<IsIlani>();
            for (int i = 0; i < TABLE_SIZE; i++)
            {
                HashChainEntry hashChainEntry = table[i];
                while (hashChainEntry != null)
                {
                    Ilanlar.Add((IsIlani)hashChainEntry.Deger);
                    hashChainEntry = hashChainEntry.Next;

                }

            }
            return Ilanlar;
        }
      
    }
}
