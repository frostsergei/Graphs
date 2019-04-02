using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class Graph : ICloneable
    {
        HashSet<Edge> edges = new HashSet<Edge>();
        HashSet<int> nodes = new HashSet<int>();

        private void refreshNodes()
        {
            nodes.Clear();

            foreach (var e in edges)
            {
                nodes.Add(e.x1);
                nodes.Add(e.x2);
            }
        }

        public Edge getRandomEdge()
        {
            Random rand = new Random();
            while (true)
            {
                foreach (Edge e in edges)
                {
                    if (rand.Next(100) < 50)
                    {
                        return e;
                    }
                }
            }
        }

        public Edge getQuaziRandomEdge()
        {
            while (true)
            {
                foreach (Edge e in edges)
                {
                    return e;
                }
            }
        }

        public int nodeCount()
        {
            return nodes.Count;
        }

        public int edgeCount()
        {
            return edges.Count;
        }

        public void addOneEdge(int x1, int x2)
        {
            Edge edge = new Edge(x1, x2);
            edges.Add(edge);
            nodes.Add(x1);
            nodes.Add(x2);
        }

        public void addEdge(int x1, int x2, bool b)
        {
            if (b && (x1 == x2)) return;
            addOneEdge(x1, x2);
            addOneEdge(x2, x1);
        }

        public void removeOneEdge(int x1, int x2)
        {
            Edge edge = new Edge(x1, x2);
            edges.Remove(edge);
            refreshNodes();
        }

        public void removeEdge(int x1, int x2)
        {
            removeOneEdge(x1, x2);
            removeOneEdge(x2, x1);
        }

        public void removeNode(int x)
        {
            HashSet<Edge> fit = new HashSet<Edge>();
            foreach (Edge e in edges)
            {
                if (e.containsNode(x))
                {
                    fit.Add(e);
                }
            }
            edges.ExceptWith(fit);
            refreshNodes();
        }

        public int nodeOutputsNumber(int x)
        {
            int n = 0;
            foreach (Edge e in edges)
            {
                if (e.isStartFor(x))
                {
                    ++n;
                }
            }
            return n;
        }

        public int maxNodeOutputNumber()
        {
            int n = 0;
            foreach (var node in nodes)
            {
                if (nodeOutputsNumber(node) > n) n = nodeOutputsNumber(node);
            }
            return n;
        }

        public int[] getAllFinish(int node)
        {
            List<int> a = new List<int>();
            foreach (var n in nodes)
            {
                foreach (var e in edges)
                {
                    if (e.isStartFor(n)) a.Add(e.getFinish());
                }
            }
            return a.ToArray<int>();
        }

        public int[] nodesSelectedOutputNumber(int x)
        {
            List<int> a = new List<int>();
            foreach (var n in nodes)
            {
                if (nodeOutputsNumber(n) == x) a.Add(n);
            }
            return a.ToArray<int>();
        }

        public void clearEdges()
        {
            edges.Clear();
            nodes.Clear();
        }

        public string outputEdges()
        {
            string log = string.Empty;
            foreach (var e in edges)
            {
                log += e.x1 + " -> " + e.x2 + "\n";
            }
            return log;
        }

        public string outputList()
        {
            string log = string.Empty;
            foreach (var node in nodes)
            {
                log += node + " -> ";
                foreach (var edge in edges)
                {
                    if (edge.isStartFor(node)) log += edge.getFinish() + "  ";
                }
                log += "\n";
            }
            return log;
        }

        public string tempResults()
        {
            string str = string.Empty;
            str += "Промежуточные результаты:\n";
            str += "Рёбра:\n" + outputEdges() + "\n";
            str += "Список смежности:\n" + outputList() + "\n";
            str += "\n";
            return str;
        }

        public object Clone()
        {
            return new Graph
            {
                edges = new HashSet<Edge>(this.edges),
                nodes = new HashSet<int>(this.nodes)
            };
        }
    }
}
