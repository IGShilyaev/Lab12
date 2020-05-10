﻿using System;
using ClassLibrary10;

namespace Lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            Organization org1 = new Organization("A", 1950);
            Organization org2 = new Organization("B", 1955);
            Library lib = new Library("C", 1860, 150);
            ShipComp sp = new ShipComp("D", 1800, 300);
            Organization[] arr = { org1, org2, lib, sp };

            #region List1Demo
            MyList1<Organization> list1 = new MyList1<Organization>();
            list1.AddtoBegin(sp);
            list1.AddtoBegin(lib);
            list1.AddtoBegin(org2);
            list1.AddtoBegin(org1);

            Console.WriteLine("Сформированный однонаправленный список:");
            list1.PrintList();

            Organization delete = null;
            foreach (Organization x in list1) if (x.Founded % 2 == 0) delete =(Organization) x.Clone();
            if (delete != null) list1.RemoveKey(delete);
            else Console.WriteLine("В коллекции не было найдено четных элементов");

            Console.WriteLine();
            Console.WriteLine("Однонаправленный список после обработки:");
            list1.PrintList();

            list1 = null;
            Console.WriteLine("Список удален из мамяти, для продолжения нажмите Enter");
            Console.ReadLine();
            Console.Clear();
            #endregion

            #region list2Demo

            MyList2<Organization> list2 = new MyList2<Organization>(arr);
            Console.WriteLine("Сформированный двунаправленный список");
            list2.PrintList();

            Console.WriteLine("Введите номер позиции, на которую хотите вставить новый элемент");
            int pos = EnterNatNumber();
            Library addedLib = new Library("NEW!!", 1860, 150);
            list2.AddtoPosition(addedLib, pos);

            Console.WriteLine();
            Console.WriteLine("Двунаправленный список после обработки:");
            list2.PrintList();

            list2 = null;
            Console.WriteLine("Список удален из мамяти, для продолжения нажмите Enter");
            Console.ReadLine();
            Console.Clear();
            #endregion

            #region TreesDemo
            MyIdealTree<Organization> idealTree = new MyIdealTree<Organization>(arr);
            Console.WriteLine("Сформированное идеально сбалансированное дерево:");
            idealTree.Show();

            Organization min = null;
            int minValue = 2021; 
            foreach(Organization x in idealTree) if(x.Founded<minValue) { minValue = x.Founded; min = x; }
            Console.WriteLine("Минимальный элемент в дереве:");
            min.Show();

            MyFindTree<Organization> findTree = new MyFindTree<Organization>();
            findTree.root = new PointFindTree<Organization>(idealTree.root.data);
            foreach (Organization x in idealTree) findTree.Add(findTree.root, x);
            Console.WriteLine("Дерево поиска, сформированное на основе идеально сбалансированного");
            findTree.Show();

            min = null;
            minValue = 2021;
            foreach(Organization x in findTree) if (x.Founded < minValue) { minValue = x.Founded; min = x; }
            Console.WriteLine("Минимальный элемент в дереве:");
            min.Show();

            idealTree = null;
            findTree = null;
            Console.WriteLine("Деревья удалены из памяти, для продолжения нажмите Enter");
            Console.ReadLine();
            Console.Clear();
            #endregion

            #region CircListDemo
            CircList<Organization> cList = new CircList<Organization>();
            cList.PrintList();
            cList.AddtoBegin(org1);
            cList.PrintList();
            cList.AddtoEnd(sp);

            Organization[] mass2 = { org2, lib };
            cList.AddtoBegin(mass2);
            cList.PrintList();
            cList.RemoveKey(sp);

            cList.PrintList();
            Console.WriteLine($"Количество элементов в списке = {cList.Length}");
            Console.WriteLine($"Первый элемент списка: {cList.Beg}");
            Console.WriteLine($"Последний элемент списка: {cList.End}");

            CircList<Organization> cloneList = (CircList<Organization>) cList.Clone();
            foreach (Organization x in cloneList) x.Name += "CLONED";

            cloneList.PrintList();
            Console.WriteLine();
            cList.PrintList();
            Console.WriteLine();

            cList = new CircList<Organization>(cloneList);
            cList.PrintList();
            Console.WriteLine();

            cList = new CircList<Organization>(arr);
            cList.PrintList();

            cList = null;
            cList = null;

            Console.WriteLine("Кольцевые списки удалены из памяти, для завершения работы нажмите Enter");
            #endregion

            Console.ReadLine();
        }

        static int EnterNatNumber()
        {
            bool ok;
            int n;
            do
            {
                ok = int.TryParse(Console.ReadLine(), out n)&&(n>0);
                if (!ok) Console.WriteLine("Ошибка ввода, введите целое положительное число");
            } while (!ok);
            return n;
        }

    }
}
