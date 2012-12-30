using System;
using System.Text;

namespace CharsAndStrings
{
    internal class EncodingExamples
    {
        public static void EncodeDecodeUTF8()
        {
            string s = "Hello there";

            Encoding encoding = Encoding.UTF8;

            byte[] encodedBytes = encoding.GetBytes(s);

            Console.WriteLine("Encoded bytes: " + BitConverter.ToString(encodedBytes));

            string decodedString = encoding.GetString(encodedBytes);

            Console.WriteLine("Decoded string: " + decodedString);
        }

        public static void Base64StringEncoding()
        {
            byte[] bytes = new byte[10];

            new Random().NextBytes(bytes);

            Console.WriteLine(BitConverter.ToString(bytes));

            var s = Convert.ToBase64String(bytes);
            Console.WriteLine(s);

            bytes = Convert.FromBase64String(s);
            Console.WriteLine(BitConverter.ToString(bytes));
        }
    }
}