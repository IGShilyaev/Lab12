using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lab12
{
    class MyList1<T>: IEnumerable<T>
    {
        Point<T> beg;

        #region numerator
        class Numerator1<T>: IEnumerator<T>
        {
            Point<T> beg;
            Point<T> current;
            public T Current
            {
                get { return current.data; }
            }

            object IEnumerator.Current
            {
                get { return current; }
            }

            public Numerator1(MyList1<T> collection)
            {
                beg = collection.Beg;
                current = null;
            }

            public void Dispose() { }

            public bool MoveNext()
            {
                if (current == null) current = beg;
                else current = current.next;
                return current != null;
            }

            public void Reset()
            {
                current = this.beg;
            }

        }
        #endregion

        public int Length
        {
            get
            {
                if (beg == null) return 0;
                int len = 0;
                Point<T> p = beg;
                while(p != null)
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

        public MyList1()
        {
            beg = null;
        }

        public MyList1(int size)
        {
            beg = new Point<T>();
            Point<T> p = beg;
            for (int i=1; i<size;i++)
            {
                Point<T> temp = new Point<T>();
                p.next = temp;
                p = temp;
            }
        }

        public MyList1(params T[] mas)
        {
            beg = new Point<T>();
            beg.data = mas[0];
            Point<T> p = beg;
            for(int i =1; i<mas.Length; i++)
            {
                Point<T> temp = new Point<T>();
                temp.data = mas[i];
                p.next = temp;
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
                Console.WriteLine(p.ToString());
                p = p.next;
            }
        }

        public void AddtoBegin(T d)
        {
            Point<T> temp = new Point<T>(d);
            if(beg == null)
            {
                beg = temp;
                return;
            }
            temp.next = beg;
            beg = temp;
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

            if(Length == 1 && !beg.data.Equals(key))
            {
                Console.WriteLine("Элемента нет в списке");
                return;
            }

            if (beg.data.Equals(key))
            {
                beg = beg.next;
                return;
            }

            Point<T> p = beg;
            while(p.next.next!=null && !p.next.data.Equals(key)) p = p.next;

            if (!p.next.data.Equals(key))
            {
                Console.WriteLine("Элемента нет в списке");
                return;
            }

            p.next = p.next.next;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Numerator1<T>(this);
        }

    }
}
