using System;
using System.Runtime.InteropServices;
using System.Security;

namespace CharsAndStrings
{
    internal class SecureStringExamples
    {
        public static void RetrieveAndShowSecureString()
        {
            using (SecureString ss = new SecureString())
            {
                Console.WriteLine("Please enter a password");

                while (true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Enter) break;

                    ss.AppendChar(cki.KeyChar);

                    Console.Write("*");                    
                }

                Console.WriteLine();

                DisplaySecureString(ss);
            }
        }

        private unsafe static void DisplaySecureString(SecureString ss)
        {
            Char* pc = null;

            try
            {
                pc = (Char*) Marshal.SecureStringToCoTaskMemUnicode(ss);

                for (var index = 0; pc[index] != 0; index++)
                    Console.Write(pc[index]);
            }
            finally
            {
                if(pc!=null)
                    Marshal.ZeroFreeCoTaskMemUnicode((IntPtr)pc);
            }
        }
    }
}