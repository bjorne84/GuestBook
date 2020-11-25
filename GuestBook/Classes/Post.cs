using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.IO; // for file
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using static System.Console;


namespace GuestBook.Classes
{
    class Post : Controller
    {
        // :::::::: Fields ::::::::::::::
        private int Id_private;
        private string Name_private;
        private string Message_private;


        // :::::::: Properties / encapsilation :::::::::::::::::
        public int Id { get => Id_private; set => Id_private = value; }
        public string Name { get => Name_private; set => Name_private = value; }
        public string Message { get => Message_private; set => Message_private = value; }


        // :::::::::::: Constructor ::::::::::::::::::::::

        public Post(int id, string name, string message)

        {
            // this means what belomngs to the class
            // On the right is from the input in construktor
            this.Id = id;
            this.Name = name;
            this.Message = message;
        }

        // :::::::::::::: Methods ::::::::::::::::::::::::



        // Start the whole program
        public static void startMethod()
        {
            WriteLine("\n:::::::::::::::::::::::::::::::::::::::::::::");
            WriteLine(":::::::::::     B J Ö R N S   ::::::::::::::::");
            WriteLine(":::::::::::  G U E S T B O O K   ::::::::::::");
            WriteLine(":::::::::::::::::::::::::::::::::::::::::::::\n\n");
            WriteLine("Använt tangenterna för att navigera\n");
            WriteLine("1. Skriv i gästboken");
            WriteLine("2. Ta bort inlägg\n");
            WriteLine("X. Avsluta\n\n");


            // Deserialize JSON to list object and show messages
            var filePath = @"C:\Users\Ny ägare\Documents\Webbprogrammering\cSharp\moment3\GuestBook\GuestBook\Files\book.json";

            // Create new empty listobjct
            List<Post> listPosts = new List<Post>();
            var inList = Post.getJson(filePath);

            // Test if listobject isn null = empty, 
            if (inList == null)
            {
                Post.WriteList(listPosts);
            }
            else
            {
                listPosts = inList;
                Post.WriteList(listPosts);
            }


            // method controlls indata att then starts the right methods, returns a string of the oprion
            string optionIN = Controller.StartCheck();


            // Method to start diferent methods depending on the options choosen by user
            Post.start(listPosts, optionIN);

        }

        public static List<Post> getJson(string filePath)
        {

                using (var tw = new StreamWriter(filePath, true))
                {
                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath);
                        string json = "[]";
                        tw.WriteLine(json);
                        tw.Close();
                    }
    
                }



            // Intake JSON
            string streamresult = null;
            using (var sr = new StreamReader(filePath, true))
            {
                streamresult = sr.ReadToEnd();
                sr.Close();
            }

            //File.WriteAllText(filePath, streamresult, Encoding.UTF8);
            // 
            //  var utf8Reader = new Utf8JsonReader(streamresult);
            List<Post> inputData = JsonConvert.DeserializeObject<List<Post>>(streamresult);
            /*foreach (var item in inputData)
            {
                Console.WriteLine("Id :" + item.Id + " Namn:" + item.Name + " Inlägg: " + item.Message + "Index: " + inputData.LastIndexOf(item));
            }*/
            return inputData;
        }

        public static void WriteList(List<Post> listPosts)
        {
            if(listPosts == null)
            {
                WriteLine("Inget meddelande är sparat");
            }
            else
            {
                foreach (var item in listPosts)
                {
                    Console.WriteLine("Id: " + listPosts.LastIndexOf(item) + " Namn:" + item.Name + " Inlägg: " + item.Message);
                }
            }
           

        }

        /// <summary>
        /// Forward users to create, delete or end program 
        /// </summary>
        /// /// <param name="listPosts" name="input"></param>
        public static void start(List<Post> listPosts, string input)
        {
            switch (input)
            {
                case "1":
                    insertNewMsg(listPosts);
                    break;
                case "2":
                    deleteList(listPosts);
                    break;
                case "X":
                    WriteLine("Du har valt att avsluta programmet");
                    Environment.Exit(0);
                    break;
            }
        }
        

        /// <summary>
        /// Delete listpost object from userinput, test that it´s valid input and then calls the savejson method
        /// </summary>
        /// /// <param name="listPosts"></param>
        public static void deleteList(List<Post> listPosts)
        {

            WriteLine("\nVälj index-nummer för det meddelande du vill radera");
        start:
            var index = ReadLine();
            int number = int.Parse(index);
            // Controll data, se if its a number
            if (!int.TryParse(index, out int output))
            {
                WriteLine("Inte ett giltig nummer, pröva igen");

                goto start;
            }
            // check that number exist in index
            if (listPosts.ElementAtOrDefault(number) != null)
            {
                // Delete by given index-number
                listPosts.RemoveAt(number);
                WriteLine("Uppdaterad lista");
                WriteList(listPosts);
            }
            else
            {
                WriteLine("Inte ett giltig nummer, pröva igen!");
                goto start;
            }

            saveJson(listPosts);

        }

        /// <summary>
        /// Inserts new messages in list objekt.
        /// </summary>
        /// /// <param name="listPosts"></param>
        public static void insertNewMsg(List<Post> listPosts)
        {
        startMsg:
            WriteLine("Gästboksinlägg\n");
            WriteLine("Skriv ditt namn\n");
            var name = ReadLine();
            WriteLine("Skriv ditt meddelande");
            var msg = ReadLine();
            int id = 1;
            WriteLine("\n\n");
            //objList.Add(new Post(id, name, msg));6

            Post post = new Post(id, name, msg);

            // Controll to se that fields are not empty
            if (String.IsNullOrEmpty(post.Name))
            {
                WriteLine("Du måste ange ett namn");
                goto startMsg;
            }
            if (String.IsNullOrEmpty(post.Message))
            {
                WriteLine("Du måste skriva ett meddelande också förstår du väl?");
                goto startMsg;
            }
            // adds new message to list
            if(listPosts == null )
            {
              
            }
            listPosts.Add(post);

            // call method to write updated list on screen
            WriteList(listPosts);

            saveJson(listPosts);
        }

        /// <summary>
        /// Method to save list to JSON-file
        /// </summary>
        /// <param name="listPosts"></param>
        public static void saveJson(List<Post> listPosts)
        {
            string json = JsonConvert.SerializeObject(listPosts, Formatting.Indented);

            //Write(json);
            // ta bort gammal json-fil

            string rootFolder = @"C:\Users\Ny ägare\Documents\Webbprogrammering\cSharp\moment3\GuestBook\GuestBook\Files\";
            string fileName = "book.json";
            try
            {
                // Check if file exists with its full path    
                if (File.Exists(Path.Combine(rootFolder, fileName)))
                {
                    // If file found, delete it    
                    File.Delete(Path.Combine(rootFolder, fileName));
                }
                else Console.WriteLine("File not found");
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }


            var filePath = @"C:\Users\Ny ägare\Documents\Webbprogrammering\cSharp\moment3\GuestBook\GuestBook\Files\book.json";
            using (var tw = new StreamWriter(filePath, true))
            {
                tw.WriteLine(json.ToString());
                tw.Close();
            }
            startMethod();
        }
    }
}
