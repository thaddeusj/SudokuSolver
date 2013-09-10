using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class Triple<T> : Tuple<T, T, T> where T : IEquatable<T>
    {

        public Triple(T item1, T item2, T item3) : base(item1, item2, item3) { }

        public T this[int i]
        {
            get
            {
                if (i == 0) return Item1;
                else if (i == 1) return Item2;
                else if (i == 2) return Item3;
                else throw new ArgumentOutOfRangeException(i.ToString());
            }

        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Triple<T>)) return false;

            return ((Triple<T>)obj).Item1.Equals(Item1) && ((Triple<T>)obj).Item2.Equals(Item2) && ((Triple<T>)obj).Item3.Equals(Item3);

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class TripleComparer<T> : IEqualityComparer<Triple<T>> where T : IEquatable<T>
    {

        public bool Equals(Triple<T> p1, Triple<T> p2)
        {
            if (p1.Item1.Equals(p2.Item1) && p1.Item2.Equals(p2.Item2) && p1.Item3.Equals(p2.Item3)) return true;

            return false;
        }

        public int GetHashCode(Triple<T> p)
        {
            return p.GetHashCode();
        }

    }
}
