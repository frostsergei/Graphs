﻿/// Author of all methods and related classes: Morozov Sergei

using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Graphs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RackpackPage : ContentPage
    {
        Dictionary<int, RackpackItem> items;

        public RackpackPage()
        {
            InitializeComponent();
            items = new Dictionary<int, RackpackItem>();
        }

        private string showItems()
        {
            string str = string.Empty;

            foreach (var item in items)
            {
                str += "Предмет " + item.Value.id + ": Вес = " + item.Value.weight + "; Цена = " + item.Value.price + "\n";
            }
            return str;
        }

        private string outMatrix(int[,] m, int x, int y)
        {
            string str = string.Empty;
            for (int i = 0; i < x + 1; i++)
            {
                for (int j = 0; j < y + 1; j++)
                {
                    str += m[j, i].ToString() + " ";
                }
                str += " n=" + i + "\n";
            }
            return str;
        }

        private async void ShowClearDialog()
        {
            var answer = await DisplayAlert("Очистка", "Вы уверены, что хотите удалить введённые данные о графе?", "Да", "Нет");
            if (answer)
            {
                items.Clear();
                dataLabel.Text = "Пусто";
            }
        }

        private bool checkData(string s1, string s2)
        {
            return CheckNumber.badData(s1, s2);
        }

        public void badDataMessage()
        {
            DisplayAlert("Ошибка", "Проверьте введённые данные!", "ОК");
        }

        private void AddClicked(object sender, EventArgs e)
        {
            if (CheckNumber.badData(textBoxID.Text, textBoxWeight.Text))
            {
                badDataMessage();
                return;
            }

            if (CheckNumber.badData(textBoxPrice.Text, textBoxWeight.Text))
            {
                badDataMessage();
                return;
            }

            RackpackItem val = new RackpackItem(Convert.ToInt32(textBoxID.Text), Convert.ToInt32(textBoxWeight.Text), Convert.ToInt32(textBoxPrice.Text));
            if (items.TryGetValue(Convert.ToInt32(textBoxID.Text), out RackpackItem v))
            {
                items[Convert.ToInt32(textBoxID.Text)] = val;
            }
            else
            {
                items.Add(Convert.ToInt32(textBoxID.Text), val);
            }
            dataLabel.Text = showItems();
        }

        private void RemoveClicked(object sender, EventArgs e)
        {
            if ((CheckNumber.badData(textBoxID.Text, textBoxWeight.Text) && (CheckNumber.badData(textBoxPrice.Text, textBoxWeight.Text))))
            {
                badDataMessage();
                return;
            }
            RackpackItem val = new RackpackItem(Convert.ToInt32(textBoxID.Text), Convert.ToInt32(textBoxWeight.Text), Convert.ToInt32(textBoxPrice.Text));
            if (items.TryGetValue(Convert.ToInt32(textBoxID.Text), out RackpackItem v))
            {
                items.Remove(Convert.ToInt32(textBoxID.Text));
            }

            dataLabel.Text = showItems();
        }

        private void ClearClicked(object sender, EventArgs e)
        {
            ShowClearDialog();
        }

        private void MinWeightClicked(object sender, EventArgs e)
        {
            Dictionary<int, RackpackItem> data = new Dictionary<int, RackpackItem>(items);
            if (CheckNumber.badData(textBoxVolume.Text, textBoxVolume.Text))
            {
                badDataMessage();
                return;
            }
            int volume = Convert.ToInt32(textBoxVolume.Text);

            int w = 0;
            int p = 0;
            string log = string.Empty;

            GreedyRackpack.GreedyMinWeight(data, volume, ref w, ref p, ref log);

            Solution.Text = log;
            Answer.Text = string.Empty;
        }

        private void MaxPriceClicked(object sender, EventArgs e)
        {
            Dictionary<int, RackpackItem> data = new Dictionary<int, RackpackItem>(items);
            if (CheckNumber.badData(textBoxVolume.Text, textBoxVolume.Text))
            {
                badDataMessage();
                return;
            }
            int volume = Convert.ToInt32(textBoxVolume.Text);

            int w = 0;
            int p = 0;
            string log = string.Empty;

            GreedyRackpack.GreedyMaxPrice(data, volume, ref w, ref p, ref log);

            Solution.Text = log;
            Answer.Text = string.Empty;
        }

        private void OptimalClicked(object sender, EventArgs e)
        {
            Dictionary<int, RackpackItem> data = new Dictionary<int, RackpackItem>(items);
            if (CheckNumber.badData(textBoxVolume.Text, textBoxVolume.Text))
            {
                badDataMessage();
                return;
            }
            int volume = Convert.ToInt32(textBoxVolume.Text);

            int w = 0;
            int p = 0;
            string log = string.Empty;

            GreedyRackpack.GreedyOptimal(data, volume, ref w, ref p, ref log);

            Solution.Text = log;
            Answer.Text = string.Empty;
        }

        private void DynamicClicked(object sender, EventArgs e)
        {
            //var ordered = items.OrderBy(x=>x.Value.weight);

            if (CheckNumber.badData(textBoxVolume.Text, textBoxVolume.Text))
            {
                badDataMessage();
                return;
            }
            int volume = Convert.ToInt32(textBoxVolume.Text);

            int[,] res = DynamicRackpack.calculateRackpack(items, volume, items.Count);

            string initData = "0 ";

            for (int i = 0; i < items.Count; i++)
            {
                initData += items.ElementAt(i).Value.weight + " ";
            }
            initData += " Веса\n0 ";
            for (int i = 0; i < items.Count; i++)
            {
                initData += items.ElementAt(i).Value.price + " ";
            }
            initData += "Стоимости\n0 ";
            for (int i = 0; i < items.Count; i++)
            {
                initData += items.ElementAt(i).Value.id + " ";
            }
            initData += "ID\n";
            Solution.Text = initData + outMatrix(res, volume, items.Count);

            List<int> r = new List<int>();

            string log = string.Empty;

            int w = 0;
            int p = 0;
            DynamicRackpack.findAnswer(items, res, items.Count, volume, ref r, ref log, ref w, ref p);

            string ans = "Результат: взяты предметы ";
            int[] array = r.ToArray();
            for (int i = 0; i < r.Count; i++)
            {
                ans += r[i].ToString() + " ";
            }

            Answer.Text = "Начинаем движение из угла\n" + log + "\n" + ans;
        }

        private void GraphClicked(object sender, EventArgs e)
        {
            Dictionary<int, RackpackItem> data = new Dictionary<int, RackpackItem>(items);
            if (CheckNumber.badData(textBoxVolume.Text, textBoxVolume.Text))
            {
                badDataMessage();
                return;
            }
            int volume = Convert.ToInt32(textBoxVolume.Text);

            string log = string.Empty;

            RackpackBranchAndBound.BranchAndBound(data, 0, volume, 0, 0, ref log);

            Solution.Text = log;
            Answer.Text = string.Empty;
        }

        private void BranchAndBoundClicked(object sender, EventArgs e)
        {
            Dictionary<int, RackpackItem> data = new Dictionary<int, RackpackItem>(items);
            if (CheckNumber.badData(textBoxVolume.Text, textBoxVolume.Text))
            {
                badDataMessage();
                return;
            }
            int volume = Convert.ToInt32(textBoxVolume.Text);

            string log = string.Empty;

            var myInts = items.Values.ToArray();
            myInts = myInts.OrderByDescending(a => (a.price / a.weight)).ToArray();

            string success = string.Empty;

            int global = 0;

            if (myInts[0].weight < volume)
            {
                BNB.start(myInts, 0, volume, 0, 0, volume * (myInts[0].price / myInts[0].weight), ref log, false, ref success, string.Empty, ref global);
            }

            Solution.Text = log;
            Answer.Text = success;
        }
    }   
}