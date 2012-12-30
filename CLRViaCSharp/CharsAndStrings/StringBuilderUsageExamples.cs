using System;
using System.Text;

namespace CharsAndStrings
{
    public class StringBuilderUsageExamples
    {
        public static void StringBuilderExamples()
        {
            //StringBuilder object contains a field that refers to an array of char structures
            //can manipulate this with methods

            StringBuilder sb = new StringBuilder();
            Console.WriteLine(sb.ToString());

            // Can chain together
            sb = new StringBuilder();
            var s = sb.AppendFormat("{0} {1}", "Simon", "Whittemore").Replace(' ', '-').Remove(3, 2)
                      .ToString();
            Console.WriteLine(s);
        }
    }
}