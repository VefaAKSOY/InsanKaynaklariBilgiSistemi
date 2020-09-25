using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İnsanKaynaklariBilgi
{
    public class HeapDugumu
    {
        public object Deger { get; set; }
        Random rnd = new Random();
        public int UygunlukDegeri { get; set; }
        public HeapDugumu(object deger)
        {
            this.Deger = deger;
            this.UygunlukDegeri = rnd.Next(0, 10);
        }
    }
}
