using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Graphs
{
    class RackpackItem : IEquatable<RackpackItem>
    {
        public int id;
        public int weight;
        public int price;

        public RackpackItem(int i, int w, int p)
        {
            id = i;
            weight = w;
            price = p;
        }

        public override int GetHashCode()
        {
            return id;
        }

        public bool Equals(RackpackItem other)
        {
            return ((id.Equals(other.id)) && (weight.Equals(other.weight)) && (price.Equals(other.price)));
        }
    }
}
