using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace İnsanKaynaklariBilgi
{
    public class HashSirketler
    {
        int TABLE_SIZE = 200;

        HashChainEntry[] table;
        public HashSirketler()
        {
            table = new HashChainEntry[TABLE_SIZE];
            for (int i = 0; i < TABLE_SIZE; i++)
                table[i] = null;
        }
        public void SirketEkle(int isyeriNo, Isyeri isyeri)
        {
            int hash = (isyeriNo % TABLE_SIZE);
            if (table[hash] == null)
                table[hash] = new HashChainEntry(isyeriNo, isyeri);
            else
            {
                HashChainEntry entry = table[hash];
                while (entry.Next != null && entry.Anahtar != isyeriNo)
                    entry = entry.Next;
                if (entry.Anahtar == isyeriNo)
                    entry.Deger = isyeri;
                else
                    entry.Next = new HashChainEntry(isyeriNo, isyeri);
            }
        }
        public Isyeri IsyeriGetir(int isYerino)
        {
            int hash = (isYerino % TABLE_SIZE);
            if (table[hash] == null)
                return null;
            else
            {
                HashChainEntry entry = table[hash];
                while (entry != null && entry.Anahtar != isYerino)
                    entry = entry.Next;
                if (entry == null)
                    return null;
                else
                    return (Isyeri)entry.Deger;
            }
        }

        public void IsyeriKaldir(int IsyeriNo)
        {
            int hash = (IsyeriNo % TABLE_SIZE);
            while (table[hash] != null && table[hash].Anahtar % TABLE_SIZE != IsyeriNo % TABLE_SIZE)
            {
                hash = (hash + 1) % TABLE_SIZE;
            }
            HashChainEntry current = table[hash];
            bool isRemoved = false;
            while (current != null)
            {
                if (current.Anahtar == IsyeriNo)
                {
                    table[hash] = current.Next;
                    isRemoved = true;
                    break;
                }

                if (current.Next != null)
                {
                    if (current.Next.Anahtar == IsyeriNo)
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
        public List<Isyeri> IsyeriListesi()
        {
            List<Isyeri> Sirketler = new List<Isyeri>();
            for (int i = 0; i < TABLE_SIZE; i++)
            {
                HashChainEntry hashChainEntry = table[i];
                while (hashChainEntry!=null)
                {
                    Sirketler.Add((Isyeri)hashChainEntry.Deger);
                    hashChainEntry = hashChainEntry.Next;

                }
                
            }
            return Sirketler;
        } 
    }
}
