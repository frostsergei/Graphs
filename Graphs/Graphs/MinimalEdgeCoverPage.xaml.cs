using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Graphs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MinimalEdgeCoverPage : ContentPage
	{
        Graph graph;

		public MinimalEdgeCoverPage ()
		{
			InitializeComponent ();
            graph = new Graph();
		}

        private void showEdges()
        {
            EdgesLabel.Text = graph.outputEdges();
        }
        private void showList()
        {
            ListLabel.Text = graph.outputList();
        }

        private void ClearGraph()
        {
            graph.clearEdges();
            showEdges();
            showList();
        }

        private async void ShowClearDialog()
        {
            var answer = await DisplayAlert("Очистка", "Вы уверены, что хотите удалить введённые данные о графе?", "Да", "Нет");
            if (answer)
            {
                ClearGraph();
            }
        }

        private bool checkData(string s1, string s2)
        {
            return CheckNumber.badData(s1, s2);
        }

        private void badDataMessage()
        {
            DisplayAlert("Ошибка", "Проверьте введённые данные!", "ОК");
        }

        private void AddEdge_Clicked(object sender, EventArgs e)
        {
            if (CheckNumber.badData(textBox1.Text, textBox2.Text))
            {
                badDataMessage();
                return;
            }
            graph.addEdge(CheckNumber.getNumber(textBox1.Text), CheckNumber.getNumber(textBox2.Text), switchNoLops.IsToggled);
            showEdges();
            showList();
        }

        private void RemoveEdge_Clicked(object sender, EventArgs e)
        {
            if (CheckNumber.badData(textBox1.Text, textBox2.Text))
            {
                badDataMessage();
                return;
            }
            graph.addEdge(CheckNumber.getNumber(textBox1.Text), CheckNumber.getNumber(textBox2.Text), switchNoLops.IsToggled);
            showEdges();
            showList();
        }

        private void ClearEdges_Clicked(object sender, EventArgs e)
        {
            ShowClearDialog();         
        }

        private void GreedyMethod_Clicked(object sender, EventArgs e)
        {
            Graph g = (Graph)graph.Clone();
            string log = string.Empty;
            List<int> res = new List<int>();

            Greedy.GreedyAlgorythm(g, ref res, ref log, switchRandom.IsToggled);

            Solution.Text = log + "\n";
            Answer.Text = "P = ";

            int[] result = res.ToArray();
            for (int i = 0; i < result.Count(); i++)
            {
                Answer.Text += result[i] + " ";
            }
        }

        private void PreciseMethod_Clicked(object sender, EventArgs e)
        {
            Graph g = (Graph)graph.Clone();
            string log = string.Empty;
            List<int> res = new List<int>();

            Precise.PreciseAlgorythm(g, ref res, ref log, switchRandom.IsToggled);

            Solution.Text = log + "\n";
            Answer.Text = "P = ";

            int[] result = res.ToArray();
            for (int i = 0; i < result.Count(); i++)
            {
                Answer.Text += result[i] + " ";
            }
        }
    }
}