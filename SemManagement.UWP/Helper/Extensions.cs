using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Helper
{
    public static class Extensions
    {
        public static Collection<T> ToCollection<T>(this IEnumerable<T> enumerable)
        {
            var collection = new Collection<T>();
            foreach (T i in enumerable)
                collection.Add(i);
            return collection;
        }

        public static IEnumerable<T> IntersectBy<T, O, P>(this IEnumerable<T> source, IEnumerable<O> other, Func<T, P> tProp, Func<O, P> oProp)
        {
            var set = new HashSet<P>(other.Select<O, P>(o => oProp(o)));

            foreach (var t in source)
                if (set.Remove(tProp(t)))
                    yield return t;
        }
    }
}
