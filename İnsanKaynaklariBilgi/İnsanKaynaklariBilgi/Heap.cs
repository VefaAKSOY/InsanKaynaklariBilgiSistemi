using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İnsanKaynaklariBilgi
{
    public class Heap
    {
        private HeapDugumu[] heapArray;
        private int maxSize;
        private int currentSize;
        public Heap(int maxHeapSize)
        {
            maxSize = maxHeapSize;
            heapArray = new HeapDugumu[maxSize];
            currentSize = 0;
        }
        public bool IsEmpty()
        {
            return currentSize == 0;
        }
        public bool Insert(Kisi value)
        {
            if (currentSize == maxSize)
                return false;
            HeapDugumu newHeapDugumu = new HeapDugumu(value);
            heapArray[currentSize] = newHeapDugumu;
            MoveToUp(currentSize++);
            return true;
        }
        public void MoveToUp(int index)
        {
            int parent = (index - 1) / 2;
            HeapDugumu bottom = heapArray[index];
            while (index > 0 && heapArray[parent].UygunlukDegeri< bottom.UygunlukDegeri)
            {
                heapArray[index] = heapArray[parent];
                index = parent;
                parent = (parent - 1) / 2;
            }
            heapArray[index] = bottom;
        }
        public HeapDugumu RemoveMax() // Remove maximum value HeapDugumu
        {
            HeapDugumu root = heapArray[0];
            heapArray[0] = heapArray[--currentSize];
            MoveToDown(0);
            heapArray[currentSize] = null;
            return root;
        }
        public void MoveToDown(int index)
        {
            int largerChild;
            HeapDugumu top = heapArray[index];
            while (index < currentSize / 2)
            {
                int leftChild = 2 * index + 1;
                int rightChild = leftChild + 1;
                //Find larger child
                if (rightChild < currentSize && heapArray[leftChild].UygunlukDegeri < heapArray[rightChild].UygunlukDegeri)
                    largerChild = rightChild;
                else
                    largerChild = leftChild;
                if (top.UygunlukDegeri >= heapArray[largerChild].UygunlukDegeri)
                    break;
                heapArray[index] = heapArray[largerChild];
                index = largerChild;
            }
            heapArray[index] = top;
        }
        public List<Kisi> Basvur()
        {
            int sayac = 0;
            List<Kisi> BasvuranAdaylar = new List<Kisi>();
           
            while (heapArray[sayac] != null)
            {
                BasvuranAdaylar.Add((Kisi)(heapArray[sayac]).Deger);
                sayac++;
            }
            return BasvuranAdaylar;
        }
        public Kisi UygunAday()
        {
            return (Kisi)heapArray[0].Deger;
        }
    }
}
