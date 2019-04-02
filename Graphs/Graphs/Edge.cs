using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs { 

    class Edge : IEquatable<Edge>
    {
        public int x1, x2;

        public Edge(int v1, int v2)
        {
            this.x1 = v1;
            this.x2 = v2;
        }

        public int getStart() { return x1; }
        public int getFinish() { return x2; }

        public override int GetHashCode()
        {
            return 1000 * x1 + x2;
        }

        public bool Equals(Edge other)
        {
            if (other == null) return false;
            return ((x1.Equals(other.x1)) && (x2.Equals(other.x2)));
            //throw new NotImplementedException();
        }

        public bool containsNode(int x)
        {
            return ((x1 == x) || (x2 == x));
        }

        public bool isStartFor(int x)
        {
            return x == x1;
        }

        public bool isFinishFor(int x)
        {
            return x == x2;
        }
    }
}
