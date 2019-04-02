using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class Precise
    {
        public static void PreciseAlgorythm(Graph graph, ref List<int> result, ref string log, bool isRandom)
        {
            if (graph.edgeCount() == 0)
            {
                log += "Ребер не осталось, выполнение завершено.\n";
                return;
            }

            Edge e = (isRandom) ? (graph.getRandomEdge()) : (graph.getQuaziRandomEdge());
            int st = e.getStart();
            int fin = e.getFinish();

            log += "Убираем две случ. вершины, между которыми есть ребро:\n";
            log += st.ToString() + " " + fin.ToString() + "\n";

            graph.removeNode(st);
            graph.removeNode(fin);

            result.Add(st);
            result.Add(fin);

            log += graph.tempResults();

            PreciseAlgorythm(graph, ref result, ref log, isRandom);
        }
    }
}
