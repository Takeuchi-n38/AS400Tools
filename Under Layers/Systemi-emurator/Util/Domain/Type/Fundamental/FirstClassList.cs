using System.Collections.Generic;

namespace Domain.Type.Fundamental
{
    public class FirstClassList<T> : FirstClassCollection<T>
    {
        public FirstClassList(List<T> aList) : base(aList)
        {
        }

        protected List<T> ItList()
        {
            return itsList;
        }

        public T Get(int index)
        {
            return ItList()[index];
        }

        public int IndexOf(T item)
        {
            return ItList().IndexOf(item);
        }

        public List<T> List()
        {
            var tmplist = new List<T>();
            foreach (var element in this.ItList())
            {
                tmplist.Add(element);
            }
            return tmplist;
        }
    }

}
