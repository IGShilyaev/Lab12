using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab12
{
    class MyFindTree<T>: IEnumerable<T>
        where T: IComparable
    {
        public PointFindTree<T> root = null; 

        public MyFindTree(params T[] arr)
        {
            root = new PointFindTree<T>(arr[0]);
            for (int i = 1; i < arr.Length; i++)
            {
                Add(root, arr[i]);
            }

        }

        public void Add(PointFindTree<T> root, T d)
        {
            PointFindTree<T> p = root;
            PointFindTree<T> r = null;
            bool ok = false;
            while (p != null && !ok)
            {
                r = p;
                if (d.CompareTo(p.data) == 0) ok = true;
                else
               if (d.CompareTo(p.data) < 0) p = p.left;
                else p = p.right;
            }
            if (ok) return;
            PointFindTree<T> NewPoint = new PointFindTree<T>(d);
            if (d.CompareTo(r.data) < 0) r.left = NewPoint;
            else r.right = NewPoint;
        }


        static void ShowTree(PointFindTree<T> p, int l)
        {
            if (p != null)
            {
                ShowTree(p.left, l + 3);
                for (int i = 0; i < l; i++)
                    Console.Write(" ");
                Console.WriteLine(p.data);
                ShowTree(p.right, l + 3);
            }
        }

        public void Show()
        {
            if (root == null)
            {
                Console.WriteLine("Empty");
            }
            else ShowTree(root, 3);
        }

        public IEnumerator<T> InOrderTraversal()
        {
            if (root != null)
            {
                Stack<PointFindTree<T>> stack = new Stack<PointFindTree<T>>();
                PointFindTree<T> current = root;
                bool goLeftNext = true;

                stack.Push(current);

                while (stack.Count > 0)
                {
                    if (goLeftNext)
                    {
                        while (current.left != null)
                        {
                            stack.Push(current);
                            current = current.left;
                        }
                    }

                    yield return current.data;

                    if (current.right != null)
                    {
                        current = current.right;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

}

