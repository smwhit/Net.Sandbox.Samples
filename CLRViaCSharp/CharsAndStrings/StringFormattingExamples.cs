using System;
using System.Globalization;

namespace CharsAndStrings
{
    public class StringFormattingExamples
    {
        public static void BasicStringFormatting()
        {
            object o = new object();
            Console.WriteLine(o); // default implementation returns objects current value, full name of the object's type
        }

        public static void FormattableStrings()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            //Implement IFormattable
            //All the base types implement
            DateTime d = new DateTime(2012, 12, 31);
            Console.WriteLine(d.ToString("G", null));

            Console.WriteLine(d.ToString("D", new CultureInfo("de")));
            // CultureInfo uses NumberFormat and DateTimeFormat properties

            var price = 123.45m;

            Console.WriteLine(price.ToString("C", new CultureInfo("de-DE")));

            Console.WriteLine(price.ToString("C", CultureInfo.InvariantCulture));

            //three types implement IFormatProvider
            //CultureInfo
            //NumberFormatInfo
            //DateTimeFormatInfo

        }

        public static void MultipleObjectsFormatting()
        {
            var s = string.Format("On {0:D}, {1} is {2:E} years old", DateTime.Today, "Aidan", 7);
            Console.WriteLine(s);
        }
    }
}