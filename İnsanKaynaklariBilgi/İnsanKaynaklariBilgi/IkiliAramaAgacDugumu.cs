using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İnsanKaynaklariBilgi
{
   public class IkiliAramaAgacDugumu    
   {
        public Object veri;
        public IkiliAramaAgacDugumu sol;
        public IkiliAramaAgacDugumu sag;
        public IkiliAramaAgacDugumu()
        {
        }

        public IkiliAramaAgacDugumu(Object veri)
        {
            this.veri = veri;
            sol = null;
            sag = null;
        }
    }
}
