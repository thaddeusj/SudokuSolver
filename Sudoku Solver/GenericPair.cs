using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class Pair<T> : Tuple<T,T> where T : IEquatable<T>
    {

        public Pair(T item1, T item2) : base(item1, item2) { }

        public T this[int i]
        {
            get
            {
                if (i == 0) return Item1;
                else if (i == 1) return Item2;
                else throw new ArgumentOutOfRangeException(i.ToString());
            }

        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Pair<T>)) return false;

            return ((Pair<T>)obj).Item1.Equals(Item1) && ((Pair<T>)obj).Item2.Equals(Item2);

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class PairComparer<T> : IEqualityComparer<Pair<T>> where T : IEquatable<T>
    {

        public bool Equals(Pair<T> p1, Pair<T> p2)
        {
            if (p1.Item1.Equals(p2.Item1) && p1.Item2.Equals(p2.Item2)) return true;

            return false;
        }

        public int GetHashCode(Pair<T> p)
        {
            return p.GetHashCode();
        }

    }
}
