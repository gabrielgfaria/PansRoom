﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Pan_s_Room.Models;

namespace Pan_s_Room.Repository
{
    public class DiscRepository : IDiscRepository
    {
        private string _connectionString = @"./resources/Discs.txt";
        
        public List<Disc> GetDiscs()
        {
            var jDiscs = File.ReadAllText(_connectionString);
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
