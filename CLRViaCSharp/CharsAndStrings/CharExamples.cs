using System;

namespace CharsAndStrings
{
    public class CharUsage
    {
        public static void CharConversions()
        {
            int n;

            char c = (Char) 65;
            Console.WriteLine(c);

            n = c;
            Console.WriteLine(n);

            c = unchecked((char) (65536 + 65));
            Console.WriteLine(c);

            c = Convert.ToChar(65);

            Console.WriteLine(c);

            try
            {
                c = Convert.ToChar(70000);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Cannot convert 70000 to a char");
            }

            c = ((IConvertible) 65).ToChar(null);
            Console.WriteLine(c);

            n = ((IConvertible) c).ToInt32(null);
            Console.WriteLine(n);
        }

        public static void CharExamples()
        {
            Console.WriteLine(Char.MinValue); // '\0'
            Console.WriteLine(Char.MaxValue); // '\uffff'

            Console.WriteLine(Char.GetUnicodeCategory('o')); //lowercase letter
            Console.WriteLine(Char.GetUnicodeCategory('$')); // currency symbol

            Console.WriteLine(Char.IsDigit('1'));
            
            var lowerX = Char.ToLowerInvariant('X'); //culture insensitive
            lowerX = Char.ToLower('X'); // use CurrentCulture property of System.Threading.Thread class

            var x = Char.Parse("X");
            //x = Char.Parse("XX"); // Throws FormatException

            Double d;
            d = Char.GetNumericValue('\u0033');
            Console.WriteLine(d);
            d = Char.GetNumericValue('\u00bc'); // 1/4
            Console.WriteLine(d);
            d = Char.GetNumericValue('X'); //not numeric - returns -1.0
            Console.WriteLine(d);


        }
    }
}