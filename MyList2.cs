using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab12
{
    class MyList2<T>: IEnumerable<T>
    {
        Point<T> beg = null;

        public int Length
        {
            get
            {
                if (beg == null) return 0;
                int len = 0;
                Point<T> p = beg;
                while (p != null)
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
                while (p.next != null)
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

        public MyList2()
        {
            beg = null;
        }

        public MyList2(int size)
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
        }

        public MyList2(params T[] mas)
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
        }

        public void PrintList()
        {
            if (beg == null)
            {
                Console.WriteLine("Пустой список");
                return;
            }
            Point<T> p = beg;
            while (p != null)
            {
                Console.WriteLine(p);
                p = p.next;
            }
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
        }

        public void AddtoEnd(T d)
        {
            Point<T> temp = new Point<T>(d);
            if(beg == null)
            {
                beg = temp;
                return;
            }
            Point<T> p = End;
            p.next = temp;
            temp.pred = p;
        }

        public void AddtoPosition(T d, int position)
        {
            int i = 1;
            Point<T> temp = new Point<T>(d);
            Point<T> p = Beg;
            while (i != position && p.next != null) { i++; p = p.next; }
            if (i == position - 1) { this.AddtoEnd(temp.data); return; }
            if (i == 1){ this.AddtoBegin(temp.data); return; }
            
            p = p.pred;
            if (i < position) Console.WriteLine("Невозможно добавить элемент на заданную позицию");
            else if (p.next == null) { temp.pred = p; p.next = temp; }
            else { p.next.pred = temp; temp.next = p.next; temp.pred = p; p.next = temp; }
            
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
            while (current != null)
            {
                yield return current.data;
                current = current.next;
            }
        }

    }
}
