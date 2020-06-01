using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab12
{
    class MyIdealTree<T>: IEnumerable<T>
    {
        public PointIdealTree<T> root;
        private static int i;

        public int Count
        {
            get { int i = 0; foreach (T x in this) i++; return i; }

        }

        public MyIdealTree(params T[] arr)
        {
            root = new PointIdealTree<T>(arr[0]);
            i = 1;
            IdealTree(arr.Length, root, arr);
        }

        static PointIdealTree<T> IdealTree(int size, PointIdealTree<T> p, params T[] arr)
        {
            if (size == 0) { p = null; return p; }
            int nl, nr;
            nl = size / 2; nr = size - nl - 1;
            if (p == null)
            {
                p = new PointIdealTree<T>(arr[i]);
                i++;
            }
            p.left = IdealTree(nl, p.left, arr);
            p.right = IdealTree(nr, p.right, arr);
            return p;
        }

        static void ShowTree(PointIdealTree<T> p, int l)
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
            else
                ShowTree(root, 5);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        public IEnumerator<T> InOrderTraversal()
        {
            if (root != null)
            {
                Stack<PointIdealTree<T>> stack = new Stack<PointIdealTree<T>>();
                PointIdealTree<T> current = root;
                bool goLeftNext = true;

                stack.Push(current);

                while(stack.Count > 0)
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
    }
}
