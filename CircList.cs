using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lab12
{
    class CircList<T>: IEnumerable<T>, ICloneable, IDisposable
    {
        Point<T> beg = null;

        public int Length
        {
            get
            {
                if (beg == null) return 0;
                int len = 1;
                Point<T> p = beg.next;
                while (p != beg)
                {
                    p = p.next;
                    len++;
                }
                return len;
            }
        }

        public Point<T> End
        {
            get
            {
                if (beg == null) return beg;
                Point<T> p = beg;
                while (p.next != beg)
                {
                    p = p.next;
                }
                return p;
            }
        }

        public Point<T> Beg
        {
            get { return beg; }
            set { beg = value; }
        }

        public CircList()
        {
            beg = null;
        }

        public CircList(int size)
        {
            beg = new Point<T>();
            Point<T> p = beg;
            for (int i = 1; i < size; i++)
            {
                Point<T> temp = new Point<T>();
                p.next = temp;
                temp.pred = p;
                p = temp;
            }
            p.next = beg;
            beg.pred = p;
        }

        public CircList(params T[] mas)
        {
            beg = new Point<T>();
            beg.data = mas[0];
            Point<T> p = beg;
            for (int i = 1; i < mas.Length; i++)
            {
                Point<T> temp = new Point<T>();
                temp.data = mas[i];
                p.next = temp;
                temp.pred = p;
                p = temp;
            }
            p.next = beg;
            beg.pred = p;
        }

        public CircList(CircList<T> c)
        {
            foreach (T x in c) { AddtoEnd(x); }
        }

        public void PrintList()
        {
            if (beg == null)
            {
                Console.WriteLine("Пустой список");
                return;
            }
            Console.WriteLine(beg);
            Point<T> p = beg.next;
            while (p != beg)
            {
                Console.WriteLine(p);
                p = p.next;
            }
        }

        public void AddtoEnd(T d)
        {
            Point<T> temp = new Point<T>(d);
            if (beg == null)
            {
                beg = temp;
                return;
            }
            beg.pred = temp;
            temp.next = beg;
            temp.pred = End;
            End.next = temp;

        }

        public void AddtoBegin(T d)
        {
            Point<T> temp = new Point<T>(d);
            if (beg == null)
            {
                beg = temp;
                return;
            }
            temp.next = beg;
            beg.pred = temp;
            beg = temp;
            beg.pred = End;
            End.next = beg;
        }

        public void AddtoBegin(params T[] mass)
        {
            if (beg==null) 
            {
                beg = new Point<T>();
                beg.data = mass[0];
                Point<T> p = beg;
                for (int i = 1; i < mass.Length; i++)
                {
                    Point<T> temp = new Point<T>();
                    temp.data = mass[i];
                    p.next = temp;
                    temp.pred = p;
                    p = temp;
                }
                beg.pred = p;
            }
            else for(int i = mass.Length-1; i>=0; i--)
                {
                    Point<T> temp = new Point<T>(mass[i]);
                    temp.next = beg;
                    beg.pred = temp;
                    beg = temp;
                    beg.pred = End;
                    End.next = beg;
                }

        }


        public void RemoveKey(T key)
        {
            if (Length == 0)
            {
                Console.WriteLine("Список пустой");
                return;
            }

            if (Length == 1 && beg.data.Equals(key))
            {
                beg = null;
                return;
            }

            if (Length == 1 && !beg.data.Equals(key))
            {
                Console.WriteLine("Элемента нет в списке");
                return;
            }

            if (beg.data.Equals(key))
            {
                beg.next.pred = null;
                beg = beg.next;
                return;
            }

            Point<T> p = beg;
            while (p.next.next != null && !p.next.data.Equals(key)) p = p.next;

            if (!p.next.data.Equals(key))
            {
                Console.WriteLine("Элемента нет в списке");
                return;
            }

            p.next.next.pred = p;
            p.next = p.next.next;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            Point<T> current = beg;
            yield return current.data;
            current = current.next;
            while (current != beg)
            {
                yield return current.data;
                current = current.next;
            }
        }

         public void Dispose() { }
            
        public object Clone()
        {
            object collection = new CircList<T>(this);
            return collection;
        }
    }
}
