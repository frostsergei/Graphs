using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs
{
    class BNB
    {
        private static void Solve(RackpackItem[] data, int selected, int w, int currentW, int currentP, int currentU, ref string log, bool select, ref string found, string path, ref int globalU)
        {
            log += path + " W=" + currentW + "; V=" + currentP + "; U=" + currentU;
            if (currentU < globalU)
            {
                log += "\n";
                return;
            }

            if (currentW > w)
            {
                log += " Перебор!\n";
                return;
            }

            if ((currentW > w) || (currentU <= currentP))
            {
                log += "\n";

                if ((currentU <= currentP) || (currentU > globalU))
                {
                    log += "Найден набор предметов: " + path + "\n";
                    found = path;
                    globalU = currentU;
                }

                return;
            }

            if (selected >= data.Length)
            {
                log += " Откат!\n";
                return;
            }

            log += "\n";

            if (selected < data.Length)
            {
                int newSelectedW = currentW + data[selected].weight;
                int newSelectedP = currentP + data[selected].price;
                int newU = (selected != data.Length - 1) ? (newSelectedP + (data[selected + 1].price / data[selected + 1].weight) * (w - newSelectedW)) : (newSelectedP);

                int noNewW = currentW;
                int noNewP = currentP;
                int noNewU = (selected != data.Length - 1) ? (noNewP + (data[selected + 1].price / data[selected + 1].weight) * (w - noNewW)) : (noNewP);

                //if (newSelectedW <= w)
                Solve(data, selected + 1, w, newSelectedW, newSelectedP, newU, ref log, true, ref found, path + " +" + data[selected].id, ref globalU);
                Solve(data, selected + 1, w, noNewW, noNewP, noNewU, ref log, false, ref found, path + " -" + data[selected].id, ref globalU);
            }
        }


        public static void start(RackpackItem[] data, int selected, int w, int currentW, int currentP, int currentU, ref string log, bool select, ref string found, string path, ref int globalU)
        {
            if (data[selected].weight < w)
            {
                int newSelectedW = currentW + data[selected].weight;
                int newSelectedP = currentP + data[selected].price;
                int newU = (selected != data.Length - 1) ? (newSelectedP + (data[selected + 1].price / data[selected + 1].weight) * (w - newSelectedW)) : (newSelectedP);

                int noNewW = currentW;
                int noNewP = currentP;
                int noNewU = (selected != data.Length - 1) ? (noNewP + (data[selected + 1].price / data[selected + 1].weight) * (w - noNewW)) : (noNewP);

                log += "W= " + currentW + "; V= " + currentP + "; U= " + currentU + ";\n";
                Solve(data, 1, w, 0 + data[selected].weight, 0 + data[selected].price, newU, ref log, true, ref found, path + " +" + data[selected].id, ref globalU);
                Solve(data, 1, w, 0, 0, noNewU, ref log, false, ref found, path + " -" + data[selected].id, ref globalU);
            }
        }
    }
}
