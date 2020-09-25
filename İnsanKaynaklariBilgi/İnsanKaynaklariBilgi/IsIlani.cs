using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace İnsanKaynaklariBilgi
{
    public class IsIlani
    {
        public string ArananPozisyon { get; set; }
        public int IlanNo { get; set; }
        public string IsTanimi { get; set; }
        public string ArananOzellikler { get; set; }
        public Heap UygunAdayHeap { get; set; }
        public IsIlani()
        {
            UygunAdayHeap = new Heap(10);
        }

    }
}
