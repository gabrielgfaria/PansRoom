using System;
using Pan_s_Room.Models;
using Pan_s_Room.Repository;
using Pan_s_Room.Service;

namespace Pan_s_Room
{
    class Program
    {
        private static IDiscOperations _discOperations;
        private static IDiscRepository _discRepository;
        
        static void Main(string[] args)
        {
            _discRepository = new DiscRepository();
            _discOperations = new DiscOperations(_discRepository);
            
            ResizeWindow();
            WelcomeUser();
            ShowMenu();
        }

        private static void ShowMenu()
        {
            var option = "";
            while(option != "exit")
            {
                Console.WriteLine("Choose one of the following options:");
                Console.WriteLine("1- Register a new record");
                Console.WriteLine("2- See all registered records");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        RegisterNewRecord();
                        Console.WriteLine();
                        break;
                    case "2":
                        RetrieveAllRecords();
                        Console.WriteLine();
                        break;
                    case "exit":
                        break;
                    default:
                        break;
                }
            }
        }

        private static void RetrieveAllRecords()
        {
            var discs = _discOperations.GetDiscs();
            foreach(var disc in discs)
            {
                Console.WriteLine(disc.Name);
            }
        }

        private static void RegisterNewRecord()
        {
            var disc = new Disc();
            var artist = new Artist();
            Console.Write("Record Name: ");
            disc.Name = Console.ReadLine();
            Console.Write("Band or Artist: ");
            artist.Name = Console.ReadLine();
            disc.Artist = artist;
            Console.Write("Record Year: ");
            disc.Year = Convert.ToInt32(Console.ReadLine());

            _discOperations.AddDisc(disc);
        }

        private static void ResizeWindow()
        {
            Console.WindowWidth = 100;
        }

        private static void WelcomeUser()
        {
            var welcomeTextArray = new[]
            {
                @" #     #                                                                         ",
                @" #  #  # ###### #       ####   ####  #    # ######    #####  ####                ",
                @" #  #  # #      #      #    # #    # ##  ## #           #   #    #               ",
                @" #  #  # #####  #      #      #    # # ## # #####       #   #    #               ",
                @" #  #  # #      #      #      #    # #    # #           #   #    #               ",
                @" #  #  # #      #      #    # #    # #    # #           #   #    #               ",
                @"  ## ##  ###### ######  ####   ####  #    # ######      #    ####                ",
                @"                                                                                 ",
                @"                ######                ###           ######                       ",
                @"                #     #   ##   #    # ###  ####     #     #  ####   ####  #    # ",
                @"                #     #  #  #  ##   #  #  #         #     # #    # #    # ##  ## ",
                @"                ######  #    # # #  # #    ####     ######  #    # #    # # ## # ",
                @"                #       ###### #  # #          #    #   #   #    # #    # #    # ",
                @"                #       #    # #   ##     #    #    #    #  #    # #    # #    # ",
                @"                #       #    # #    #      ####     #     #  ####   ####  #    # ",
            };
            Console.WriteLine("\n\n");
            foreach (string line in welcomeTextArray)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("\n\n");
        }
    }
}
