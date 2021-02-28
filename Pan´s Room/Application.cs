﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTables;
using Models;
using Repository;
using Services;

namespace Pan_s_Room
{
    public class Application : IApplication
    {
        private readonly IDiscServices _discServices;
        private readonly IDiscRepository _discRepository;

        public Application(IDiscServices discServices, IDiscRepository discRepository)
        {
            _discServices = discServices;
            _discRepository = discRepository;
        }

        public void Run()
        {
            ResizeWindow();
            WelcomeUser();
            ShowMenu();
        }

        private void ShowMenu()
        {
            var option = "";
            while (option != "exit")
            {
                Console.WriteLine("Choose one of the following options:");
                Console.WriteLine("1- Register a new record");
                Console.WriteLine("2- See all registered records");
                Console.WriteLine("Or type 'exit' to quit the application");
                Console.WriteLine();
                Console.Write("You choose option: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ClearScreen();
                        RegisterNewRecord();
                        ClearScreen();
                        break;
                    case "2":
                        ClearScreen();
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

        private void RetrieveAllRecords()
        {
            var discs = _discServices.GetDiscs()
                .OrderBy(d => d.Artist.Name)
                .ThenBy(d => d.Year)
                .ToList();
            WriteTable(discs);
        }

        private void RegisterNewRecord()
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

            Console.WriteLine("You´re about to add this disc to your collection:\n");
            WriteTable(new List<Disc>() { disc });
            Console.WriteLine();
            Console.WriteLine("Is it correct? [Y] or [N]");
            var answer = Console.ReadLine();
            
            if(answer.ToLower() == "y" || answer.ToLower() == "yes")
            {
                var addedDisc = _discServices.AddDisc(disc);
                Console.WriteLine("The disc was added to your collection!\n");
            }
            else if (answer.ToLower() == "n" || answer.ToLower() == "no")
            {
                RegisterNewRecord();
            }

        }

        private void ResizeWindow()
        {
            Console.WindowWidth = 90;
        }

        private void WelcomeUser()
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

        private static void WriteTable(List<Disc> discs)
        {
            var table = new ConsoleTable("Disc", "Artist", "Year");

            foreach (var disc in discs)
            {
                table.AddRow(disc.Name, disc.Artist.Name, disc.Year);
            }

            table.Write();
        }

        private void ClearScreen()
        {
            Console.Clear();
            WelcomeUser();
        }
    }
}
