﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using Models;
using Repository;
using Services;

namespace Pan_s_Room
{
    public class Application : IApplication
    {
        private readonly IServiceBase _discServices;
        private readonly IServiceBase _wishListServices;

        public Application(IDiscServices discServices,
            IWishListServices wishListServices)
        {
            _discServices = (DiscServices)discServices;
            _wishListServices = wishListServices;
        }

        public void Run()
        {
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
                Console.WriteLine("2- Add a new record to your wish-list");
                Console.WriteLine("3- See all registered records");
                Console.WriteLine("4- See all records on your wish-list");
                Console.WriteLine("\nOr type 'exit' to quit the application");
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
                        RegisterNewRecordToWishList();
                        ClearScreen();
                        break;
                    case "3":
                        ClearScreen();
                        RetrieveAllRecords();
                        Console.WriteLine();
                        break;
                    case "4":
                        ClearScreen();
                        RetrieveAllRecordsFromWishList();
                        Console.WriteLine();
                        break;
                    case "exit":
                        break;
                    default:
                        break;
                }
            }
        }

        private void RetrieveAllRecordsFromWishList()
        {
            var wishListDiscs = _wishListServices.GetDiscs()
                .OrderBy(d => d.Artist.Name.ToLower().Replace("the", ""))
                .ThenBy(d => d.Year)
                .ToList();
            var discs = _discServices.GetDiscs();

            WriteTable(TransformIntoWishList(wishListDiscs, discs));
        }

        private WishList TransformIntoWishList(List<Disc> wishListDiscs, List<Disc> discs)
        {
            var wishList = new WishList();
            wishList.Discs = new List<WishListDisc>();

            foreach(var wishListDisc in wishListDiscs)
            {
                var discInWishList = new WishListDisc();
                discInWishList.Name = wishListDisc.Name;
                discInWishList.Artist = wishListDisc.Artist;
                discInWishList.Year = wishListDisc.Year;
                discInWishList.AlreadyInCollection = discs.Any(d => d.Name == wishListDisc.Name && d.Artist.Name == wishListDisc.Artist.Name);

                wishList.Discs.Add(discInWishList);
            }
            return wishList;
        }

        private void RegisterNewRecordToWishList()
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

            if (answer.ToLower() == "y" || answer.ToLower() == "yes")
            {
                try
                {
                    _wishListServices.AddDisc(disc);
                }
                catch
                {
                    Console.WriteLine("You already own this disc. Would you like to continue anyways? [Y] or [N]");
                    var saveAnyways = Console.ReadLine();
                    if(saveAnyways.ToLower() == "y" || saveAnyways.ToLower() == "yes")
                    {
                        _wishListServices.AddDiscAnyways(disc);
                        ClearScreen();
                        Console.WriteLine("The disc was added to your wishlist!");
                        Task.WaitAll(Task.Delay(3000));
                    }
                }
            }
            else if (answer.ToLower() == "n" || answer.ToLower() == "no")
            {
                RegisterNewRecordToWishList();
            }
        }

        private void RetrieveAllRecords()
        {
            var discs = _discServices.GetDiscs()
                .OrderBy(d => d.Artist.Name.ToLower().Replace("the", ""))
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
                try
                {
                    _discServices.AddDisc(disc);
                }
                catch (ExistingDiscInCollectionException)
                {
                    Console.WriteLine("You already own this disc. Would you like to continue anyways? [Y] or [N]");
                    var saveAnyways = Console.ReadLine();
                    if (saveAnyways.ToLower() == "y" || saveAnyways.ToLower() == "yes")
                    {
                        _discServices.AddDiscAnyways(disc);
                        ClearScreen();
                        Console.WriteLine("The disc was added to your collection!");
                        Task.WaitAll(Task.Delay(3000));
                    }
                }
            }
            else if (answer.ToLower() == "n" || answer.ToLower() == "no")
            {
                RegisterNewRecord();
            }
        }

        private void ResizeWindow(int windowSize)
        {
            Console.WindowWidth = windowSize;
        }

        private void WelcomeUser()
        {
            ResizeWindow(90);
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

        private void WriteTable(List<Disc> discs)
        {
            var table = new ConsoleTable("Disc", "Artist", "Year");

            foreach (var disc in discs)
            {
                table.AddRow(disc.Name, disc.Artist.Name, disc.Year);
            }
            var maxRowSize = table.ToString().Split("\n").Select(el => el.Length).Max();
            if (maxRowSize > 90)
                ResizeWindow(maxRowSize);

            table.Write();
        }

        private void WriteTable(WishList discs)
        {
            var table = new ConsoleTable("Disc", "Artist", "Year", "Already In Collection");

            foreach (var disc in discs.Discs)
            {
                table.AddRow(disc.Name,
                    disc.Artist.Name,
                    disc.Year,
                    disc.AlreadyInCollection == true ? "Yes" : "No");
            }

            var maxRowSize = table.ToString().Split("\n").Select(el => el.Length).Max();
            if (maxRowSize > 90)
                ResizeWindow(maxRowSize);

            table.Write();
        }

        private void ClearScreen()
        {
            Console.Clear();
            WelcomeUser();
        }
    }
}
