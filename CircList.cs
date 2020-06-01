using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lab12
{
    class CircList<T>: IEnumerable<T>, ICloneable, IDisposable
        where T: ICloneable
    {
        Point<T> beg;

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
            beg = new Point<T>(mas[0]);
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
            foreach (T x in c) { AddtoEnd((T) x.Clone()); }
        }

        public void PrintList()
        {
            if (beg == null)
            {
                Console.WriteLine("Пустой список");
                return;
            }
            Console.Write(beg);
            Point<T> p = beg.next;
            while (p != beg)
            {
                Console.Write("; " + p);
                p = p.next;
            }
            Console.WriteLine();
        }

        public void AddtoEnd(T d)
        {
            Point<T> temp = new Point<T>(d);
            if (beg == null)
            {
                beg = temp;
                beg.next = beg;
                beg.pred = beg;
                return;
            }
            Point<T> end = End;
            end.next = temp;
            temp.pred = end;
            temp.next = beg;
            beg.pred = temp;

        }

        public void AddtoBegin(T d)
        {
            Point<T> temp = new Point<T>(d);
            if (beg == null)
            {
                temp.next = temp;
                temp.pred = temp;
                beg = temp;
                return;
            }
            Point<T> end = End;
            temp.next = beg;
            temp.pred = end;
            beg.pred = temp;
            end.next = temp;
            beg = temp;
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
                    AddtoBegin(mass[i]);
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
                Console.WriteLine($"Элемента {key} нет в списке");
                return;
            }

            if (beg.data.Equals(key))
            {
                Point<T> end = End;
                beg.next.pred = end;
                beg = beg.next;
                end.next = beg;
                return;
            }

            Point<T> p = beg.next;
            while (p.next.next != beg && !p.next.data.Equals(key)) p = p.next;

            if (!p.next.data.Equals(key))
            {
                Console.WriteLine($"Элемента {key} нет в списке");
                return;
            }

            p.next.next.pred = p;
            p.next = p.next.next;
        }

        public void RemoveKey(params T[] arr)
        {
            foreach (T x in arr) RemoveKey((T)x.Clone());
        }

        public bool Contains(T key)
        {
            if (Length == 0) return false;
            foreach (T x in this) if (key.Equals(x)) return true;
            return false;
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
