using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    class GreedyRackpack
    {
        public static void GreedyMinWeight(Dictionary<int, RackpackItem> items, int W, ref int currentW, ref int currentP, ref string log)
        {
            if ((currentW > W) || (items.Count == 0)) { log += "Конец!";  return; }

            int weightDiv = W - currentW;

            //items.Where(val => val.Value.weight < weightDiv);

            var toRemove = items.Where(pair => pair.Value.weight > weightDiv)
                         .Select(pair => pair.Key)
                         .ToList();

            foreach (var key in toRemove)
            {
                items.Remove(key);
            }

           // items.Where(val => val.Value.weight < weightDiv);
            if (items.Count == 0) { log += "Конец!"; return; }
            int keyOfMinWeight = items.Aggregate((x, y) => x.Value.weight < y.Value.weight ? x : y).Key;

            if (items.Count == 0) { log += "Конец!"; return; }

            int weight = items[keyOfMinWeight].weight;
            int price = items[keyOfMinWeight].price;

            currentW += weight;
            currentP += price;

            items.Remove(keyOfMinWeight);

            log += "Взяли предмет " + keyOfMinWeight + " : Вес = " + weight + ", Стоимость = " + price + ";\n";
            log += "Промежуточные итоги: Вес = " + currentW + ", Стоимость = " + currentP + ";\n";

            GreedyMinWeight(items, W, ref currentW, ref currentP, ref log);
        }

        public static void GreedyMaxPrice(Dictionary<int, RackpackItem> items, int W, ref int currentW, ref int currentP, ref string log)
        {
            if ((currentW > W) || (items.Count == 0)) { log += "Конец!"; return; }

            int weightDiv = W - currentW;

            var toRemove = items.Where(pair => pair.Value.weight > weightDiv)
                         .Select(pair => pair.Key)
                         .ToList();

            foreach (var key in toRemove)
            {
                items.Remove(key);
            }

            if (items.Count == 0) { log += "Конец!"; return; }
            int keyOfMaxPrice = items.Aggregate((x, y) => x.Value.price > y.Value.price ? x : y).Key;

            if (items.Count == 0) { log += "Конец!"; return; }

            int weight = items[keyOfMaxPrice].weight;
            int price = items[keyOfMaxPrice].price;

            currentW += weight;
            currentP += price;

            items.Remove(keyOfMaxPrice);

            log += "Взяли предмет " + keyOfMaxPrice + " : Вес = " + weight + ", Стоимость = " + price + ";\n";
            log += "Промежуточные итоги: Вес = " + currentW + ", Стоимость = " + currentP + ";\n";

            GreedyMaxPrice(items, W, ref currentW, ref currentP, ref log);
        }

        public static void GreedyOptimal(Dictionary<int, RackpackItem> items, int W, ref int currentW, ref int currentP, ref string log)
        {
            if ((currentW > W) || (items.Count == 0)) { log += "Конец!"; return; }

            int weightDiv = W - currentW;

            var toRemove = items.Where(pair => pair.Value.weight > weightDiv)
                         .Select(pair => pair.Key)
                         .ToList();

            foreach (var key in toRemove)
            {
                items.Remove(key);
            }

            if (items.Count == 0) { log += "Конец!"; return; }

            int keyOptimal = items.Aggregate((x, y) => x.Value.price/x.Value.weight > y.Value.price/y.Value.weight ? x : y).Key;

            if (items.Count == 0) { log += "Конец!"; return; }

            int weight = items[keyOptimal].weight;
            int price = items[keyOptimal].price;

            currentW += weight;
            currentP += price;

            items.Remove(keyOptimal);

            log += "Взяли предмет " + keyOptimal + " : Вес = " + weight + ", Стоимость = " + price + ";\n";
            log += "Промежуточные итоги: Вес = " + currentW + ", Стоимость = " + currentP + ";\n";

            GreedyOptimal(items, W, ref currentW, ref currentP, ref log);
        }
    }
}
