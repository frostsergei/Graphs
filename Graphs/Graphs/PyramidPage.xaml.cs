/// Author of all calculation algotithms: Kanovskiy Ilya
/// Author of design: Morozov Sergei


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
    public partial class PyramidPage : ContentPage
    {
        public PyramidPage()
        {
            InitializeComponent();
        }

        //Author: Kanovskiy Ilya
        private void CalculateClicked(object sender, EventArgs e)
        {
            //textBox1.Text = "4 4 8 5 4 3 5";
            String input = textBox1.Text;
            string[] inputs = input.Split(' ', '/', '.', ',', '!', '?', '+');
            int k = inputs.Length;
            for (int i = 0; i < inputs.Length; i++)
                if (inputs[i] == "")
                    k--;
            int[] numbers = new int[k];
            for (int i = 0, j = 0; i < numbers.Length; i++, j++)
            {
                while (inputs[j] == "")
                    j++;
                numbers[i] = int.Parse(inputs[j]);
            }
            richTextBox1.Text = "";
            textBox2.Text = "";
            int[,] v, w, forK;
            v = new int[numbers.Length, numbers.Length];
            w = new int[numbers.Length, numbers.Length];
            forK = new int[numbers.Length, numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
                for (int j = 0; j < numbers.Length; j++)
                {
                    v[i, j] = 0;
                    w[i, j] = 0;
                    forK[i, j] = 0;
                }
            for (int i = 0; i < numbers.Length; i++)
                w[0, i] = numbers[i];
            for (int i = 1; i < numbers.Length; i++)
                for (int j = 0; j < numbers.Length - i; j++)
                    w[i, j] = w[0, j] + w[i - 1, j + 1];
            for (int i = 1; i < numbers.Length; i++)
                for (int j = 0; j < numbers.Length - i; j++)
                {
                    int min = 1000000000;
                    for (k = i - 1; k >= 0; k--)
                        if (v[k, j] + v[i - k - 1, j + k + 1] < min)
                        {
                            min = v[k, j] + v[i - k - 1, j + k + 1];
                            forK[i, j] = k;
                        }
                    v[i, j] = w[i, j] + min;
                }
            string output = "n";
            for (int i = 1; i < numbers.Length; i++)
                output += "+n";
            output = reck(numbers.Length - 1, 0, forK, v, output, numbers.Length);
            string[] forSplit = output.Split('n');
            for (int i = 0; i < forSplit.Length - 1; i++)
            {
                textBox2.Text += forSplit[i] + numbers[i];
            }
            textBox2.Text += forSplit[forSplit.Length - 1];
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length - i; j++)
                {
                    richTextBox1.Text += w[i, j] + " ";
                }
                richTextBox1.Text += "\n";
                for (int j = 0; j < numbers.Length - i; j++)
                {
                    richTextBox1.Text += v[i, j] + " ";
                }
                richTextBox1.Text += "\n";
            }
        }

        //Author: Kanovskiy Ilya
        string reck(int i, int j, int[,] forK, int[,] v, string output, int length)
        {
            if (i > 0)
            {
                if (i < length - 1)
                    output = parenthesis(j, i + j, output);
                output = reck(forK[i, j], j, forK, v, output, length);
                output = reck(i - forK[i, j] - 1, j + forK[i, j] + 1, forK, v, output, length);
            }
            return output;
        }

        //Author: Kanovskiy Ilya
        string parenthesis(int a, int b, string output)
        {
            for (int i = 0, n = -1; i < output.Length; i++)
            {
                if (output[i] == 'n')
                {
                    n++;
                    if (n == a)
                    {
                        output = output.Insert(i, "(");
                        i++;
                    }
                    if (n == b)
                    {
                        output = output.Insert(i + 1, ")");
                        i++;
                    }
                }
            }
            return output;
        }

    }
}