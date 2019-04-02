using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class Greedy
    {
        public static void GreedyAlgorythm(Graph graph, ref List<int> result, ref string log, bool isRandom)
        {
            Random rand = new Random();
            if (graph.edgeCount() == 0)
            {
                log += "Ребер не осталось, выполнение завершено.\n";
                return;
            }

            int maxCount = graph.maxNodeOutputNumber();
            int[] nodesMaxCount = graph.nodesSelectedOutputNumber(maxCount);

            log += "Максимальная мощность ребра = " + maxCount + "\n";
            log += "Подходящие вершины: ";
            for (int i = 0; i < nodesMaxCount.Length; i++)
            {
                log += nodesMaxCount[i].ToString() + " ";
            }
            log += "\n";

            int l = nodesMaxCount.Length;
            int selected = (isRandom) ? (rand.Next(l - 1)) : (0);

            result.Add(nodesMaxCount[selected]);
            graph.removeNode(nodesMaxCount[selected]);
            log += "Убираем вершину " + nodesMaxCount[selected] + "\n";

            log += graph.tempResults();

            GreedyAlgorythm(graph, ref result, ref log, isRandom);
        }
    }
}
