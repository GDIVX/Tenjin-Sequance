using System;
using System.Collections.Generic;
using Combat;

namespace AI
{
    [Serializable]
    public class PriorityQueue<T>
    {
        private readonly List<T> heap = new List<T>();
        private readonly Comparison<T> comparison;

        public PriorityQueue(Comparison<T> comparison)
        {
            this.comparison = comparison ??
                              throw new ArgumentNullException(nameof(comparison), "Comparison cannot be null.");
        }

        public int Count => heap.Count;

        public void Enqueue(T item)
        {
            heap.Add(item);
            Sort();
        }

        public void Sort()
        {
            int childIndex = heap.Count - 1;
            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;
                if (comparison(heap[childIndex], heap[parentIndex]) >= 0)
                {
                    break;
                }

                Swap(childIndex, parentIndex);
                childIndex = parentIndex;
            }
        }

        public void Remove(T item)
        {
            heap.Remove(item);
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return heap[0];
        }

        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            T frontItem = heap[0];
            heap[0] = heap[^1];
            heap.RemoveAt(heap.Count - 1);

            MinHeapify(0);
            return frontItem;
        }

        public override string ToString()
        {
            string str = "";

            foreach (T i in heap)
            {
                str += $"{i.ToString()}, ";
            }

            return str;
        }

        private void MinHeapify(int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int smallest = index;

            if (left < heap.Count && comparison(heap[left], heap[index]) < 0)
            {
                smallest = left;
            }

            if (right < heap.Count && comparison(heap[right], heap[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != index)
            {
                Swap(index, smallest);
                MinHeapify(smallest);
            }
        }

        private void Swap(int i, int j)
        {
            (heap[i], heap[j]) = (heap[j], heap[i]);
        }
    }
}