using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumsAndBitFlags
{
    enum Colour
    {
        White,
        Red,
        Green,
        Blue,
        Orange
    }

    enum ColourByte : byte
    {
        White,
        Red,
        Green,
        Blue,
        Orange
    }

    [Flags]
    enum Actions
    {
        None = 0,
        Read = 1,
        Write = 2,
        ReadWrite = Read | Write,
        Delete = 4,
        Query = 8,
        Sync = 16
    }

    static class ActionsExtensions
    {
        public static bool IsSet(this Actions flags, Actions flagsToTest)
        {
            return (flags & flagsToTest) == flagsToTest;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            UtilitysOnEnums();

            PrintOutEnumRepresentations();

            GetAllEnumValues();

            DetectFlags();

            FlagsEnum();

            Console.Read();
        }

        private static void FlagsEnum()
        {
            var actions = Actions.Read | Actions.Delete;
            Console.WriteLine(actions);

            var canDelete = (actions & Actions.Delete) != 0;
            Console.WriteLine(canDelete);

            Console.WriteLine(actions.IsSet(Actions.Delete));

        }

        private static void DetectFlags()
        {
            Colour c = Colour.Blue;
            Console.WriteLine(c.HasFlag(Colour.Blue));
        }

        private static void GetAllEnumValues()
        {
            var colours = (Colour[])Enum.GetValues(typeof (Colour));
            Console.WriteLine(colours.Length);

            foreach (var colour in colours)
            {
                Console.WriteLine("{0,5:D}\t{0:G}", colour);
            }
        }

        private static void PrintOutEnumRepresentations()
        {
            Colour blue = Colour.Blue;
            Console.WriteLine(blue);                //blue general format
            Console.WriteLine(blue.ToString());     //blue general format
            Console.WriteLine(blue.ToString("G"));  //blue general format
            Console.WriteLine(blue.ToString("D"));  //3 decimal format
            Console.WriteLine(blue.ToString("X"));  //03 Hex format

            Console.WriteLine(Enum.Format(typeof(Colour), 3, "G"));

            var orange = (Colour) Enum.Parse(typeof (Colour), "orange", true);
            Console.WriteLine(orange);

            try
            {
                var brown = (Colour)Enum.Parse(typeof(Colour), "Brown", false);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Cannot parse brown");
            }

            Colour colour;
            Enum.TryParse("1", false, out colour);
            Console.WriteLine(colour);
        }

        private static void UtilitysOnEnums()
        {
            Console.WriteLine(Enum.GetUnderlyingType(typeof(ColourByte)));

            var c = Colour.Orange;
            Console.WriteLine(c.GetType().GetEnumUnderlyingType());

            Console.WriteLine(Enum.IsDefined(typeof(Colour), 1));
            Console.WriteLine(Enum.IsDefined(typeof(Colour), 100));
        }
    }
}
