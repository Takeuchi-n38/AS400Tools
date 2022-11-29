using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Types.Fundamentals
{
    public class FirstClassCollection<T> //: IEnumerable<T>
    {

        public List<T> itsList;

        public FirstClassCollection(List<T> aList)
        {
            itsList = aList;
        }

        public void Add(T item)
        {
            itsList.Add(item);
        }

        public bool Contains(T item)
        {
            return itsList.Contains(item);
        }

        public int Size()
        {
            return itsList.Count();
        }

        //TODO: Stream
        //public Stream<T> stream()
        //{
        //    return itsList.ToList();
        //}

        public bool IsEmpty()
        {
            return !itsList.Any();
        }

        ////TODO: Iterator
        //public IEnumerator<T> GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}
    }
}