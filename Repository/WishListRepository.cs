using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Models;
using Newtonsoft.Json;

namespace Repository
{
    public class WishListRepository : IWishListRepository
    {
        private string _connectionString = @"./resources/WishList.txt";

        public List<Disc> GetDiscs()
        {
            var jDiscs = "";
            if (File.Exists(_connectionString))
            {
                jDiscs = File.ReadAllText(_connectionString);
            }
            var discs = !string.IsNullOrWhiteSpace(jDiscs) ? JsonConvert.DeserializeObject<List<Disc>>(jDiscs) : new List<Disc>();

            return discs;
        }

        public void SaveDiscs(List<Disc> discs)
        {
            var jDiscs = JsonConvert.SerializeObject(discs);
            
            File.WriteAllText(_connectionString, jDiscs);
        }
    }
}
