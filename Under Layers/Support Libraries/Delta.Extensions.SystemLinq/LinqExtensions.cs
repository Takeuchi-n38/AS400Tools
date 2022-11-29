using System;
using System.Collections.Generic;

namespace System.Linq
{
    public static class LinqExtensions
    {
        //IEnumerableの拡張メソッド、指定された個数で要素をまとめて返す
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
        {
            if (chunksize <= 0) throw new ArgumentException();
            var loopCount = source.Count() / chunksize + (source.Count() % chunksize > 0 ? 1 : 0);
            foreach (var i in Enumerable.Range(0, loopCount))
            {
                yield return source.Skip(chunksize * i).Take(chunksize);
            }
        }
    }
}
