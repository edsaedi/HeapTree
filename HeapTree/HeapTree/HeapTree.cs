using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeapTree
{
    public class HeapTree<T> where T : IComparable<T>
    {
        internal T[] tree { get; set; }
        public int Count { get; private set; } = 0;
        internal IComparer<T> Comparer;
        public int Capacity => tree.Length;
        public bool IsEmpty => Count == 0;

        public HeapTree(IComparer<T> comparer)
            : this(comparer, 10)
        {

        }

        public HeapTree(int capacity)
            : this(null, capacity)
        {

        }

        public HeapTree(IComparer<T> comparer = null, int capacity = 10)
        {
            Comparer = comparer ?? Comparer<T>.Default;
            tree = new T[capacity];
            Count = 0;
        }

        public HeapTree(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default)
        {

        }

        public HeapTree(IEnumerable<T> collection, IComparer<T> comparer)
            : this(comparer, collection != null ? collection.Count() : 0)
        {
            if (collection == null)
            {
                throw new ArgumentException();
            }

            foreach (T element in collection)
            {
                Insert(element);
            }
        }

        public HeapTree(T[] collection, IComparer<T> comparer)
        {
            tree = collection;
            Count = Capacity;
            Comparer = comparer;
        }

        public void Insert(T value)
        {
            if (Count == Capacity)
            {
                IncreaseCapacity();
            }

            tree[Count] = value;

            Count++;

            HeapifyUp(Count);
        }

        internal void HeapifyUp(int index)
        {
            int parent = (index - 1) / 2;

            if (index == 0)
            {
                return;
            }

            if (Comparer.Compare(tree[index], tree[parent]) < 0)
            {
                T temp = tree[index];
                tree[index] = tree[parent];
                tree[parent] = temp;
            }

            HeapifyUp(parent);
        }

        public void IncreaseCapacity()
        {
            T[] temp = new T[Capacity * 2];
            tree.CopyTo(temp, 0);
            tree = temp;
        }
    }
}
