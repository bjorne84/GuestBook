using System;
using System.Collections.Generic;
using static System.Console;
using GuestBook.Classes;
using System.IO; // for file
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GuestBook
{
    class Program
    {
        static void Main(string[] args)

        {
            // Kicks everything "igång" as we say in Swedish.
            Post.startMethod();
        }
    }
}
