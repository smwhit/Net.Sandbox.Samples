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
            CharUsage.CharExamples();

            CharUsage.CharConversions();

            StringExamples.BasicStringExamples();

            StringExamples.StringComparisonExamples();

            StringExamples.StringInterningExamples();

            StringBuilderUsageExamples.StringBuilderExamples();

            StringFormattingExamples.BasicStringFormatting();

            StringFormattingExamples.FormattableStrings();

            StringFormattingExamples.MultipleObjectsFormatting();

            EncodingExamples.EncodeDecodeUTF8();

            EncodingExamples.Base64StringEncoding();

            //SecureStringExamples.RetrieveAndShowSecureString();

            Console.Read();
        }
    }
}
