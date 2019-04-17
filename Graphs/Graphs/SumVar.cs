using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs
{
    class SumVar
    {
        private static void Solve(int[] data, int selected, int w, int current, ref string log, bool select, ref bool found, string path)
        {
            if (found == true)
            {
                return;
            }

            log += path + " = " + current + " ";

            if (current == w)
            {
                log += "Найдено!\n";
                found = true;
                return;
            }

            if (selected >= data.Length)
            {
                log += "Откат!\n";
                return;
            }

            if (current > w)
            {
                log += "Перебор!\n";
                return;
            }

            log += "\n";

            if (selected < data.Length)
            {
                Solve(data, selected + 1, w, current + data[selected], ref log, true, ref found, path + " +" + data[selected]);
                Solve(data, selected + 1, w, current, ref log, false, ref found, path + " -" + data[selected]);
            }
        }


        public static void start(int[] data, int selected, int w, int current, ref string log, bool select, ref bool found, string path)
        {
            if (data[selected] < w)
            {
                log += current + "\n";
                Solve(data, 1, w, 0 + data[selected], ref log, true, ref found, path + " +" + data[selected]);
                Solve(data, 1, w, 0, ref log, false, ref found, path + " -" + data[selected]);
            }
        }
    }
}
