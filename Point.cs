using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Lab12
{
    class Point<T>
    {
        public T data;
        public Point<T> pred;
        public Point<T> next;

        public Point()
        {
            data = default(T);
            pred = null;
            next = null;
        }

        public Point(T d)
        {
            data = d;
            pred = null;
            next = null;
        }

        public override string ToString()
        {
            return data.ToString() + " ";
        }

    }
}
