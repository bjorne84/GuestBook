using System;
using static System.Console;
using System.Collections.Generic;
using System.Text;

namespace GuestBook.Classes
{
    class Controller
    {
        public static string StartCheck()
        {
            var input = ReadLine();
            string checker = null;
            checker = Check(input);

            return checker;
        }

        public static string Check(string input)
        {
            if (input.Length > 2)
            {
                WriteLine("Ange 1, 2 eller X .....");
                StartCheck();
                // goto start;
            }

            if (input == "1" || input == "2" || input.ToUpper() == "X")
            {

            }
            else
            {
                WriteLine("Du angav inte 1, 2 eller X .....");
                //   goto start;
                StartCheck();
            }
            return input;
        }
    }
}
