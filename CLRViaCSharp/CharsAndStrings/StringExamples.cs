using System;
using System.Globalization;

namespace CharsAndStrings
{
    public class StringExamples
    {
        public static void StringInterningExamples()
        {
            var s1 = "Hello";
            var s2 = "Hello";

            Console.WriteLine(ReferenceEquals(s1, s2)); // prints true, CLR interns strings automatically

            //explicitly intern

            s1 = String.Intern(s1);

            // CompilerServices.CompilationRelaxations.NoStringInterning is ignored in CLR4
        }

        public static void StringComparisonExamples()
        {
            //when comparing strings for sorting, recommended that methods on the string class are used

            //performing linguistically correct comparisons
            string s1 = "Strasse";
            string s2 = "Straße";

            var eq = String.Compare(s1, s2, StringComparison.Ordinal) == 0;
            Console.WriteLine("Are equal ordinal {0} {2} {1}", s1, s2, eq ? "==" : "!=");

            CultureInfo ci = new CultureInfo("de-DE");

            eq = String.Compare(s1, s2, true, ci) == 0;

            Console.WriteLine("Culture comparison {0} {2} {1}", s1, s2, eq ? "==" : "!=");
        }

        public static void BasicStringExamples()
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
    }
}