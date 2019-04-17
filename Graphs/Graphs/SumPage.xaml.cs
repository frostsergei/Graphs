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
	public partial class SumPage : ContentPage
	{
		public SumPage ()
		{
			InitializeComponent ();
		}

        private void CalculateClicked(object sender, EventArgs e)
        {
            if (CheckNumber.badData(textBoxVal.Text, textBoxVal.Text))
            {
                badDataMessage();
                return;
            }

            string log = string.Empty;
            string[] val = textBoxData.Text.Split(' ', '/', '.', ',', '!', '?', '+');
            val = val.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            int[] myInts = Array.ConvertAll(val, s => int.Parse(s));

            Array.Sort(myInts); //Ascending

            bool success = false;



            if (myInts[0] < CheckNumber.getNumber(textBoxVal.Text))
            {
                SumVar.start(myInts, 0, CheckNumber.getNumber(textBoxVal.Text), 0, ref log, false, ref success, string.Empty);
            }

            richTextBoxSolution.Text = log;
            textBoxAnswer.Text = (success)?("Найдено!"):("Не найдено!");
        }

        public void badDataMessage()
        {
            DisplayAlert("Ошибка", "Проверьте введённые данные!", "ОК");
        }
    }
}