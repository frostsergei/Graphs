using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    class RackpackBranchAndBound
    {
        private static Stack<int> taken = new Stack<int>();

        public static void BranchAndBound(Dictionary<int, RackpackItem> items, int step, int W, int currentW, int currentP, ref string log)
        {
            string movement = string.Empty;
            for (int i = 0; i < step; i++)
                movement += "-";

            if (currentW > W) { log += movement + "Перебор!\n";  return; }

            foreach (var item in items)
            {
                Dictionary<int, RackpackItem> copy = new Dictionary<int, RackpackItem>(items);
                taken.Push(item.Value.id);

                var toRemove = copy.Where(pair => pair.Value.id == item.Value.id).Select(pair => pair.Key).ToList();

                foreach (var key in toRemove)
                {
                    copy.Remove(key);
                }

                if (currentW + item.Value.weight <= W)
                {
                    /*Stack<int> takenCopy = new Stack<int>(taken);
                    Stack<int> rev = new Stack<int>();

                    while (takenCopy.Count != 0)
                    {
                        rev.Push(takenCopy.Pop());
                    }*/

                    log += movement + "(";
                    foreach (var v in taken)
                    {
                        log += v + " ";
                    }
                    log += "); W = " + (currentW + item.Value.weight) + "; V = " + (currentP + item.Value.price) + ";\n";

                    BranchAndBound(copy, step + 1, W, currentW + item.Value.weight, currentP + item.Value.price, ref log);
                }

                taken.Pop();
            }

        }
    }
}
