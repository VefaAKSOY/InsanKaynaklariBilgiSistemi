using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İnsanKaynaklariBilgi
{
    public class LinkedList:ListADT
    {
        public override void InsertFirst(object value)
        {
            Node tmpHead = new Node
            {
                Data = value
            };

            if (Head == null)
                Head = tmpHead;
            else
            {
                
                tmpHead.Next = Head;
                
                Head = tmpHead;
            }
            
            Size++;
        }

        public override void InsertLast(object value)
        {
           
            Node oldLast = Head;

            if (Head == null)
                
                InsertFirst(value);
            else
            {
                
                Node newLast = new Node
                {
                    Data = value
                };

               
                while (oldLast.Next != null)
                {

                    oldLast = oldLast.Next;

                }

                
                oldLast.Next = newLast;

                
                Size++;
            }
        }

        public override void InsertPos(int position, object value)
        {
            Node eklenecekeleman = new Node

            {
                Data = value
            };
            if (Head == null)
            {
                Head = eklenecekeleman;

            }



            else
            {

                Node temp = Head;
                if (position > Size)
                    System.Windows.Forms.MessageBox.Show("Dizi Size'indan buyuk deger girdiniz");
                else if (position == 1)
                    InsertFirst(value);
                else
                {

                    for (int i = 1; i < position - 1; i++)
                    {

                        temp = temp.Next;

                    }
                    eklenecekeleman.Next = temp.Next;
                    temp.Next = eklenecekeleman;

                }
                Size++;

            }

        }

        public override void DeleteFirst()
        {
            if (Head != null)
            {
                
                Node tempHeadNext = this.Head.Next;
               
                if (tempHeadNext == null)
                    Head = null;
                else
                    
                    Head = tempHeadNext;
                
                Size--;
            }
        }

        public override void DeleteLast()
        {
           
            Node lastNode = Head;
           
            Node lastPrevNode = null;
            while (lastNode.Next != null)
            {

                lastPrevNode = lastNode;
                lastNode = lastNode.Next;

            }
           
            Size--;
            
            lastNode = null;

         
            if (lastPrevNode != null)
                lastPrevNode.Next = null;
            else
                Head = null;
        }

        public override void DeletePos(int position)
        {
            Node previous = new Node();

            if (position == 1)
                DeleteFirst();

            else if (Head == null)
            {
                Head = previous;

            }



            else
            {

                Node temp = Head;
                if (position > Size)
                    System.Windows.Forms.MessageBox.Show("Dizi Size'indan buyuk deger girdiniz");

                else
                {

                    for (int i = 0; i < position - 1; i++)
                    {
                        previous = temp;
                        temp = temp.Next;

                    }
                    previous.Next = temp.Next;




                }
                Size--;
            }
        }

        public override Node GetElement(int position)
        {
           
            Node retNode = null;
            
            Node tempNode = Head;
            int count = 0;

            while (tempNode != null)
            {
                if (count == position)
                {
                    retNode = tempNode;
                    break;
                }
                
                tempNode = tempNode.Next;
                count++;
            }
            return retNode;
        }

        public override string DisplayElements()
        {
            string temp = "";
            Node item = Head;
            while (item != null)
            {
                temp += "-->" + item.Data;
                item = item.Next;
            }

            return temp;
        }

    }
}

