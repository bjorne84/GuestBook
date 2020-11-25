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
            WriteLine("\n:::::::::::::::::::::::::::::::::::::::::::::");
            WriteLine(":::::::::::     B J Ö R N S   ::::::::::::::::");
            WriteLine(":::::::::::  G U E S T B O O K   ::::::::::::");
            WriteLine(":::::::::::::::::::::::::::::::::::::::::::::\n\n");
            WriteLine("Använt tangenterna för att navigera\n");
            WriteLine("1. Skriv i gästboken");
            WriteLine("2. Ta bort inlägg\n");
            WriteLine("X. Avsluta");


            // Deserialize JSON to list object and show messages
            var filePath = @"C:\Users\Ny ägare\Documents\Webbprogrammering\cSharp\moment3\GuestBook\GuestBook\Files\book.json";

            List<Post> listPosts = new List<Post>();
            var inList = Post.getJson(filePath);
            if (inList == null)
            {
                Post.WriteList(listPosts);
            } else
            {
                listPosts = inList;
                Post.WriteList(listPosts);
            }
       

            // method controlls indata att then starts the right methods, returns a string of the oprion
            string optionIN = Controller.StartCheck();


            // Method to start diferent methods depending on the options choosen by user
            Post.start(listPosts, optionIN);

        }
    }
}
