using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort_Task
{
    public static class Sorter
    {
        /* * * * Quick sort | Recursive method * * * */
        /* Launcher */
        public static T[] QuickRecursive<T>(T[] input, int startIndex, int endIndex, Func<T, T, int> comparer)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (startIndex < 0 || startIndex > endIndex)
                throw new ArgumentException($"Incorrect range:{startIndex}, {endIndex}.");
            if (comparer == null)
                throw new NullReferenceException(nameof(comparer));

            var array = new T[input.Length];
            input.CopyTo(array, 0);
            QuickRecursiveAlgorithm(array, startIndex, endIndex, comparer);
            return array;
        }
        /* Launcher */
        public static T[] QuickRecursive<T>(T[] input, Func<T, T, int> comparer)
            => QuickRecursive(input, 0, Math.Max(0, input.Length - 1), comparer);
        private static void QuickRecursiveAlgorithm<T>(T[] array, int startIndex, int endIndex, Func<T, T, int> comparer)
        {
            void Swap(int indexA, int indexB)
            {
                T temp = array[indexA];
                array[indexA] = array[indexB];
                array[indexB] = temp;
            }

            if (endIndex <= startIndex)
                return;

            int XElemIndex = endIndex;
            int currentIndex = startIndex;

            for (int i = startIndex; i < endIndex; i++)
            {
                if (comparer(array[i], array[XElemIndex]) <= 0)
                {
                    Swap(currentIndex, i);
                    ++currentIndex;
                }
            }

            Swap(currentIndex, XElemIndex);
            QuickRecursiveAlgorithm(array, startIndex, currentIndex - 1, comparer);
            QuickRecursiveAlgorithm(array, currentIndex + 1, endIndex, comparer);
        }



        /* * * * Quick sort | Iterative method * * * */
        /* Launcher */
        public static T[] QuickIterative<T>(T[] input, int startIndex, int endIndex, Func<T, T, int> comparer)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (startIndex < 0 || startIndex > endIndex)
                throw new ArgumentException($"Incorrect range:{startIndex}, {endIndex}.");
            if (comparer == null)
                throw new NullReferenceException(nameof(comparer));

            var array = new T[input.Length];
            input.CopyTo(array, 0);
            QuickIterativeAlgorithm(array, startIndex, endIndex, comparer);
            return array;
        }
        /* Launcher */
        public static T[] QuickIterative<T>(T[] input, Func<T, T, int> comparer)
           => QuickIterative(input, 0, Math.Max(0, input.Length - 1), comparer);
        private static void QuickIterativeAlgorithm<T>(T[] array, int startIndex, int endIndex, Func<T, T, int> comparer)
        {
            void Swap(int indexA, int indexB)
            {
                T temp = array[indexA];
                array[indexA] = array[indexB];
                array[indexB] = temp;
            }

            (int start, int end) current = (startIndex, endIndex);

            Stack<(int start, int end)> stack = new();
            stack.Push(current);

            while (stack.Count != 0) 
            {
                current = stack.Pop();

                if (current.end <= current.start)
                    continue;

                int XElemIndex = current.end;
                int currentIndex = current.start;

                for (int i = current.start; i < current.end; i++)
                {
                    if (comparer(array[i], array[XElemIndex]) <= 0)
                    {
                        Swap(currentIndex, i);
                        ++currentIndex;
                    }
                }
                Swap(currentIndex, XElemIndex);

                stack.Push((current.start, currentIndex - 1));
                stack.Push((currentIndex + 1, current.end));
            }
        }



        /* * * * Piramidal sort * * * */
        /* Launcher */
        public static T[] Piramidal<T>(T[] input, Func<T, T, int> comparer)
            => Piramidal(input, 0, Math.Max(input?.Length - 1 ?? 0, 0), comparer);
        /* Launcher */
        public static T[] Piramidal<T>(T[] input, int startIndex, int endIndex, Func<T, T, int> comparer)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (startIndex < 0 || startIndex > endIndex)
                throw new ArgumentException($"Incorrect range:{startIndex}, {endIndex}.");
            if (comparer == null)
                throw new NullReferenceException(nameof(comparer));

            var array = new T[input.Length];
            input.CopyTo(array, 0);
            return PiramidalAlgorithm(array, startIndex, endIndex, comparer);
        }
        private static T[] PiramidalAlgorithm<T>(T[] input, int startIndex, int endIndex, Func<T, T, int> comparer)
        {
            int rangeL = endIndex - startIndex;

            /* Bringint to a heap */
            for (int i = startIndex + rangeL / 2; i >= startIndex; --i)
                MakeHeap(input, i, startIndex, endIndex, comparer);
            /* Sorting process */
            for (int i = startIndex + rangeL; i >= startIndex + 1; --i)
            {
                T temp = input[startIndex];
                input[startIndex] = input[i];
                input[i] = temp;

                MakeHeap(input, startIndex, startIndex, i - 1, comparer);
            }

            return input;
        }
        private static void MakeHeap<T>(T[] input, int fatherNodeIndex, int startIndex, int border, Func<T, T, int> comparer)
        {
            while ((fatherNodeIndex - startIndex) * 2 + 1 <= border - startIndex)
            {
                /* Search the largest of two descendants */
                int firstDesIndex = (fatherNodeIndex - startIndex) * 2 + 1 + startIndex;
                int maxDescendantIndex = firstDesIndex;
                if (firstDesIndex + 1 <= border && comparer(input[firstDesIndex], input[firstDesIndex + 1]) < 1)
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
}
