using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityTools.Collections
{
    [Serializable]
    public class SimpleObservableList<T> : IEnumerable<T>
    {
        public event Action Changed;
        public event Action Changing;

        public event Action<T> Added;
        public event Action<T> Removed;

        private readonly List<T> _originalList;

        public int Count => _originalList.Count;

        public T this[int index]
        {
            get
            {
                return _originalList[index];
            }
            set
            {
                Changing?.Invoke();
                _originalList[index] = value;
                Changed?.Invoke();
            }
        }

        public SimpleObservableList()
        {
            _originalList = new List<T>();
        }

        public SimpleObservableList(IEnumerable<T> collection)
        {
            _originalList = new List<T>(collection);
        }

        public void Add(T element)
        {
            Changing?.Invoke();
            _originalList.Add(element);
            Added?.Invoke(element);
            Changed?.Invoke();
        }

        public void Remove(T element)
        {
            Changing?.Invoke();
            _originalList.Remove(element);
            Removed?.Invoke(element);
            Changed?.Invoke();
        }

        public void Insert(int index, T element)
        {
            Changing?.Invoke();
            _originalList.Insert(index, element);
            Added?.Invoke(element);
            Changed?.Invoke();
        }

        public int IndexOf(T element)
        {
            return _originalList.IndexOf(element);
        }

        public void RemoveAt(int index)
        {
            Changing?.Invoke();
            Removed?.Invoke(_originalList.ElementAt(index));
            _originalList.RemoveAt(index);
            Changed?.Invoke();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_originalList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_originalList).GetEnumerator();
        }
    }
}