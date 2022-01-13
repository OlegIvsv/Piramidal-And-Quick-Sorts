using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Sort_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<int[]> print = (arr1) =>
            {
                foreach (var item in arr1)
                    Console.Write(item.ToString() + ", ");
            };

            var people = new Person[]
            {
                new(){Age = 23, Name = "Ivan1", Lastname="a", Cash = 10000 },
                new(){Age = 45, Name = "Ivan2", Lastname="c", Cash = 10 },
                new(){Age = 32, Name = "Ivan3", Lastname="b", Cash = 780 },
                new(){Age = 19, Name = "Ivan4", Lastname="e", Cash = 102 },
                new(){Age = 101, Name = "Ivan5", Lastname="d", Cash = 88 },
                new(){Age = 47, Name = "Ivan6", Lastname="g", Cash = 1 },
                new(){Age = 47, Name = "Ivan6", Lastname="f", Cash = 1 },
                new(){Age = 47, Name = "Ivan6", Lastname="t", Cash = 1 },
                new(){Age = 47, Name = "Ivan6", Lastname="w", Cash = 1 },
            };

            //var res = Sorter.QuickRecursive(people, (x, y) => y.Age - x.Age);

            //print(res);

            for (int i = 0; i < 200; ++i)
            {
                int[] arr = new int[i];
                Random rand = new();
                for (int j = 0; j < arr.Length; j++)
                {
                    arr[j] = rand.Next(1000);
                }
                int a, b;
                a = rand.Next(arr.Length);
                b = rand.Next(a, arr.Length);
                arr = Sort.Piramidal(arr, a, b, (x, y) => x - y);
                bool o = IsOrdered(arr, a, b);
                Console.WriteLine(o);
                if (!o)
                {
                    print(arr);
                    Console.WriteLine("\n" + a + " " + b);
                    break;
                }
                if (i == 1 || i == 5 || i == 30 || i == 40 || i ==55 )
                {
                    print(arr);
                    Console.WriteLine(a + " " + b);
                }
            }


            //var sorted = Sort.Piramidal(people,0, (x, y) => x.Lastname.CompareTo(y.Lastname));
            //print(sorted);
            //Console.WriteLine(  );
            //print(people.OrderBy(x => x.Lastname));
        }

        static bool IsOrdered(int[] arr, int a, int b)
        {
            if (arr.Length == 0 || a == b)
                return true;
            for (int i = a; i <= b; i++)
            {
                if (arr[Math.Min(i + 1, Math.Min(arr.Length - 1, b))] < arr[i])
                    return false;
            }
            return true;
        }
        static bool IsOrdered(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i + 1] < arr[i])
                    return false;
            }
            return true;
        }
    }

    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public double Cash { get; set; }

        public override string ToString()
        {
            return $"{Age} - {Name} {Lastname} - {Cash}";
        }
    }
}


public static class Sort
{
    public static T[] Piramidal<T>(T[] input, int startIndex, int endIndex, Func<T, T, int> comparer)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));
        if (comparer == null)
            throw new NullReferenceException(nameof(comparer));

        int rangeL = endIndex - startIndex + 1;

        var array = new T[input.Length];
        input.CopyTo(array, 0);
        /* Bringint to a heap */
        for (int i = startIndex + rangeL / 2 - 1; i >= startIndex; --i)
            MakeHeap(array, i, startIndex, endIndex, comparer);
        /* Sorting process */
        for (int i = startIndex + rangeL - 1; i >= startIndex + 1; --i)
        {
            T temp = array[startIndex];
            array[startIndex] = array[i];
            array[i] = temp;

            MakeHeap(array, startIndex, startIndex, i - 1, comparer);
        }

        return array;
    }
    private static void MakeHeap<T>(T[] input, int fatherNodeIndex, int startIndex, int border, Func<T, T, int> comparer)
    {
        while ((fatherNodeIndex - startIndex) * 2 + 1 <= border - startIndex)
        {
            /* Search the largest of two descendants */
            int maxDescendantIndex = (fatherNodeIndex - startIndex) * 2 + 1 + startIndex;
            if ((fatherNodeIndex - startIndex) * 2 + 2 + startIndex <= border
                && comparer(input[(fatherNodeIndex - startIndex) * 2 + 1 + startIndex], input[(fatherNodeIndex - startIndex) * 2 + 2 + startIndex]) < 1)
                ++maxDescendantIndex;
            /* Swap if necessary */
            if (comparer(input[maxDescendantIndex], input[fatherNodeIndex]) > 0)
            {
                T temp = input[maxDescendantIndex];
                input[maxDescendantIndex] = input[fatherNodeIndex];
                input[fatherNodeIndex] = temp;
                /* Proceed to descendant */
                fatherNodeIndex = maxDescendantIndex;
            }
            else
                break;
        }
    }
}
