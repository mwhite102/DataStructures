using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Sorting
{
    public static class SortingAlgorithms<T> where T : IComparable<T>
    {
        /// <summary>
        /// Common swap method
        /// </summary>
        /// <param name="items"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private static void Swap(T[] items, int left, int right)
        {
            if (left != right)
            {
                T temp = items[left];
                items[left] = items[right];
                items[right] = temp;
            }
        }

        #region Bubble Sort

        /// <summary>
        /// Performs a bubble sort on the input array
        /// </summary>
        /// <param name="items">The array of items to be sorted</param>
        public static void BubbleSort(T[] items)
        {
            if (items == null) return;

            bool swapped;

            do
            {
                swapped = false;
                for (int i = 1; i < items.Length; i++)
                {
                    if (items[i - 1].CompareTo(items[i]) > 0)
                    {
                        Swap(items, i - 1, i);
                        swapped = true;
                    }
                }
            } while (swapped != false);
        }

        #endregion

        #region InsertionSort

        /// <summary>
        /// Performs an insertion sort on the input array
        /// </summary>
        /// <param name="items">The array of items to be sorted</param>
        public static void InsertionSort(T[] items)
        {
            int sortedRangeEndIndex = 1;

            while (sortedRangeEndIndex < items.Length)
            {
                if (items[sortedRangeEndIndex].CompareTo(items[sortedRangeEndIndex - 1]) < 0)
                {
                    int insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex]);
                    Insert(items, insertIndex, sortedRangeEndIndex);
                }

                sortedRangeEndIndex++;
            }
        }

        private static int FindInsertionIndex(T[] items, T valueToInsert)
        {
            for (int index = 0; index < items.Length; index++)
            {
                if (items[index].CompareTo(valueToInsert) > 0)
                {
                    return index;
                }
            }

            throw new InvalidOperationException("Insertion index not found");
        }

        private static void Insert(T[] items, int indexInsertingAt, int indexInsertingFrom)
        {
            // Store value in indexInsertingAt into a temp value
            T temp = items[indexInsertingAt];

            // Set value in indexInsertingAt to value in indexInsertingFrom
            items[indexInsertingAt] = items[indexInsertingFrom];

            // Shift items left working backwards from indexInsertingFrom
            for (int current = indexInsertingFrom; current > indexInsertingAt; current--)
            {
                items[current] = items[current - 1];
            }

            // Set indexInsertingAt + 1 to the temp value
            items[indexInsertingAt + 1] = temp;
        }

        #endregion

        #region SelectionSort

        /// <summary>
        /// Performs a selection sort on the input array
        /// </summary>
        /// <param name="items">The array of items to be sorted</param>
        public static void SelectionSort(T[] items)
        {
            int sortedRangeEnd = 0;

            while (sortedRangeEnd < items.Length)
            {
                int nextIndex = FindIndexOfSmallestFromIndex(items, sortedRangeEnd);
                Swap(items, sortedRangeEnd, nextIndex);

                sortedRangeEnd++;
            }
        }

        private static int FindIndexOfSmallestFromIndex(T[] items, int sortedRangeEnd)
        {
            T currentSmallest = items[sortedRangeEnd];
            int currentSmallestIndex = sortedRangeEnd;

            for (int i = sortedRangeEnd + 1; i < items.Length; i++)
            {
                if (currentSmallest.CompareTo(items[i]) > 0)
                {
                    currentSmallest = items[i];
                    currentSmallestIndex = i;
                }
            }

            return currentSmallestIndex;
        }

        #endregion

        #region MergeSort

        /// <summary>
        /// Performs a merge sort on the input array
        /// </summary>
        /// <param name="items">The array of items to be sorted</param>
        public static void MergeSort(T[] items)
        {
            if (items.Length <= 1) return;

            int leftSize = items.Length / 2;
            int rightSize = items.Length - leftSize;

            T[] left = new T[leftSize];
            T[] right = new T[rightSize];

            Array.Copy(items, 0, left, 0, leftSize);
            Array.Copy(items, leftSize, right, 0, rightSize);

            // Recursive Call
            MergeSort(left);
            MergeSort(right);

            Merge(items, left, right);
        }

        private static void Merge(T[] items, T[] left, T[] right)
        {
            int leftIndex = 0;
            int rightIndex = 0;
            int targetIndex = 0;

            int remaining = left.Length + right.Length;

            while(remaining > 0)
            {
                if (leftIndex >= left.Length)
                {
                    items[targetIndex] = right[rightIndex++];
                }
                else if (rightIndex >= right.Length)
                {
                    items[targetIndex] = left[leftIndex++];
                }
                else if (left[leftIndex].CompareTo(right[rightIndex]) < 0)
                {
                    items[targetIndex] = left[leftIndex++];
                }
                else
                {
                    items[targetIndex] = right[rightIndex++];
                }

                targetIndex++;
                remaining--;
            }
        }

        #endregion

        #region QuickSort

        static Random _pivotRng = new Random();

        public static void QuickSort(T[] items)
        {
            DoQuickSort(items, 0, items.Length - 1);
        }

        private static void DoQuickSort(T[] items, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = _pivotRng.Next(left, right);
                int newPivot = Partition(items, left, right, pivotIndex);

                // Recursive calls
                DoQuickSort(items, left, newPivot - 1);
                DoQuickSort(items, newPivot + 1, right);
            }
        }

        private static int Partition(T[] items, int left, int right, int pivotIndex)
        {
            T pivotValue = items[pivotIndex];

            Swap(items, pivotIndex, right);

            int storeIndex = left;

            for (int i  = left; i < right; i++)
            {
                if (items[i].CompareTo(pivotValue) < 0)
                {
                    Swap(items, i, storeIndex);
                    storeIndex += 1;
                }
            }

            Swap(items, storeIndex, right);
            return storeIndex;
        }

        #endregion
    }
}
