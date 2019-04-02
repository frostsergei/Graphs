using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs
{
    class CheckNumber
    {
        public static bool isNumber(string s)
        {
            bool b = int.TryParse(s, out int i);
            return b;
        }

        public static int getNumber(string s)
        {
            int i = 0;
            bool b = int.TryParse(s, out i);
            return i;
        }

        private static bool isNumber(string str1, string str2)
        {
            bool b = ((isNumber(str1)) && (isNumber(str2)));
            return b;
        }

        private static bool checkEmpty(string str1, string str2)
        {
            bool b = ((str1 == null) || (str2 == null));
            return b;
        }

        public static bool badData(string str1, string str2)
        {
            bool b1 = checkEmpty(str1, str2);
            bool b2 = isNumber(str1, str2);
            bool b = (b1 ? true : !b2);
            //if (b) DisplayAlert("Warning!", "Заполните пустые строки и проверьте корректность введённых чисел!", "Сейчас проверю!");
            return b;
        }
    }
}
