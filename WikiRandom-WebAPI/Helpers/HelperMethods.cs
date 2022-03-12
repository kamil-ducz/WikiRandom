using System;
using System.Collections.Generic;

namespace Services
{
    public class HelperMethods
    {
        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int start, end;
                start = strSource.IndexOf(strStart, 0) + strStart.Length;
                end = strSource.IndexOf(strEnd, start);
                return strSource.Substring(start, end - start);
            }
            else return "";
        }

        public static void DisplayStringList(List<string> inputList)
        {
            foreach (var input in inputList)
            {
                int currentIndex = inputList.IndexOf(input);
                Console.WriteLine($"{currentIndex}.{input}\n");
            }
        }

    }
}

