using System;
using System.Globalization;
using System.Text;

namespace CharsAndStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            //characters are always represented in 16bit unicode values
            //char is a value type
            CharExamples();

            CharConversions();

            BasicStringExamples();

            StringComparisonExamples();

            StringInterningExamples();

            StringBuilderExamples();

            Console.Read();
        }

        private static void StringBuilderExamples()
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

        private static void StringInterningExamples()
        {
            var s1 = "Hello";
            var s2 = "Hello";

            Console.WriteLine(ReferenceEquals(s1, s2)); // prints true, CLR interns strings automatically

            //explicitly intern

            s1 = string.Intern(s1);

            // CompilerServices.CompilationRelaxations.NoStringInterning is ignored in CLR4
        }

        private static void StringComparisonExamples()
        {
            //when comparing strings for sorting, recommended that methods on the string class are used

            //performing linguistically correct comparisons
            string s1 = "Strasse";
            string s2 = "Straße";

            var eq = string.Compare(s1, s2, StringComparison.Ordinal) == 0;
            Console.WriteLine("Are equal ordinal {0} {2} {1}", s1, s2, eq ? "==" : "!=");

            CultureInfo ci = new CultureInfo("de-DE");

            eq = string.Compare(s1, s2, true, ci) == 0;

            Console.WriteLine("Culture comparison {0} {2} {1}", s1, s2, eq ? "==" : "!=");
        }

        private static void BasicStringExamples()
        {
            //immutable ordered set of characters
            //derives from System.Object
            //implements IComparable/IComparable<string>, ICloneable, IEnumerable/IEnumerable<string>, and IEquatable<string>

            //verbatim string
            string path = @"c:\windows\xyz.exe";

            //immutable strings, cannot change.
            //No need for thread synchronisation, can share multiple string contents through a single object (string interning
            //CLR access fields in string type directly for performance reasons

        }

        private static void CharConversions()
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

        private static void CharExamples()
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
