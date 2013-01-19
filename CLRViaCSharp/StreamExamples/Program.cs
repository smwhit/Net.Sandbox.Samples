using System;
using System.IO;

namespace Tasks
{
    class StreamExamples
    {
        public static void Main(string[] args)
        {
            DirExample(args);

            ReadWriteBinary();

            //AppendAndReadLog();

            ReadTextFromAMemoryStream();

            //ReadCharactersFromAString(); 
        }

        private static void ReadTextFromAMemoryStream()
        {
            using (var ms = new MemoryStream())
            {
                var sw = new StreamWriter(ms);
                sw.WriteLine("Hello mates");
                sw.WriteLine("Goodbye mates");
                sw.Flush();

                using (StreamReader sr = new StreamReader(ms))
                {
                    ms.Position = 0;
                    //var str = sr.ReadToEnd(); 
                    string str;
                    while ((str = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(str);
                    }
                }

                sw.Close();
            }
        }

        private static void ReadCharactersFromAString()
        {
            var str = "Some number of characters";

            char[] b = new char[str.Length];

            var sr = new StringReader(str);
            sr.Read(b, 0, 13);

            Console.WriteLine(b);

            sr.Read(b, 5, str.Length - 13);

            Console.WriteLine(b);
        }

        private static void AppendAndReadLog()
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                Log("Test 1", w);
                Log("Test 2", w);
                w.Close();
            }

            using (StreamReader r = File.OpenText("log.txt"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
                r.Close();
            }
        }

        private static void Log(string message, TextWriter w)
        {
            w.Write("\r\nLog Entry: ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.WriteLine(" :");
            w.WriteLine(" :{0}", message);
            w.WriteLine("----------------------------");
            w.Flush();
        }

        private static void ReadWriteBinary()
        {
            using (FileStream fs = new FileStream("XYZ.bin", FileMode.Create))
            {
                using (BinaryWriter w = new BinaryWriter(fs))
                {
                    for (var i = 0; i < 11; i++)
                    {
                        w.Write(i);
                    }
                }
            }

            using (FileStream fs = new FileStream("XYZ.bin", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader r = new BinaryReader(fs))
                {
                    //for(int i = 0; i < 11; i++) 
                    //{ 
                    //    Console.WriteLine(r.ReadInt32()); 
                    //}

                    while (r.PeekChar() != -1)
                    {
                        Console.WriteLine(r.ReadInt32());
                    }
                }
            }
        }

        private static void DirExample(string[] args)
        {
            string path = Environment.CurrentDirectory;

            if (args.Length > 0)
            {
                if (Directory.Exists(args[0]))
                {
                    path = args[0];
                }
                else
                {
                    Console.WriteLine("{0} not found; using current directory", args[0]);
                }
            }

            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (var fileInfo in dir.GetFiles("*.exe"))
            {
                Console.WriteLine("{0, -12:N0} {1,-20:g} {2}", fileInfo.Length, fileInfo.CreationTime, fileInfo.Name);
            }
        }
    }
}
