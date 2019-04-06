using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    class DynamicRackpack
    {
        private static int max(int x1, int x2)
        { return (x1>x2) ? (x1) : (x2); }

        public static int[,] calculateRackpack(Dictionary<int,RackpackItem> items, int rackpackWeight, int itemsNumber)
        {
            int[,] matrix = new int[itemsNumber + 1, rackpackWeight + 1];

            for (int i = 1; i < itemsNumber + 1; i++)
                for (int j = 1; j < rackpackWeight; j++)
                    matrix[i, j] = 0;

            for (int i = 1; i < itemsNumber+1; i++)
                for (int j = 1; j <= rackpackWeight; j++)
                    if (j >= items.ElementAt(i-1).Value.weight)
                        matrix[i, j] = max(matrix[i-1,j],matrix[i-1,j-items.ElementAt(i-1).Value.weight]+items.ElementAt(i-1).Value.price);
                    else
                        matrix[i, j] = matrix[i-1,j];

            return matrix;
        }

        public static void findAnswer(Dictionary<int, RackpackItem> items, int[,] matrix, int k ,int s, ref List<int> result, ref string log, ref int w, ref int p)
        {
            if (matrix[k, s] == 0)
            {
                log += "Кoнец! \nВес рюкзака = " + w + ";\nСтоимость рюкзака = " + p + "\n";
                return;
            }

            if (matrix[k - 1, s] == matrix[k, s])
            {
                log += matrix[k - 1, s] + "==" + matrix[k, s] + "->\n";
                findAnswer(items, matrix, k - 1, s, ref result, ref log, ref w, ref p);
            }
            else
            {
                //Might be logical mistake here, it does not work well with lecture version. Wiki version works great, however. 
                log += matrix[k - 1, s] + "!=" + matrix[k, s] + "=> Добавляем " + items.ElementAt(k - 1).Value.id + "; сдвиг на " + items.ElementAt(k - 1).Value.weight + "\n";
                p += items.ElementAt(k - 1).Value.price;
                w += items.ElementAt(k - 1).Value.weight;
                findAnswer(items, matrix, k - 1, s - items.ElementAt(k - 1).Value.weight, ref result, ref log, ref w, ref p);
                result.Add(items.ElementAt(k - 1).Value.id);
            }
        }
    }
}
