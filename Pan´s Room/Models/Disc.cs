using System;
using System.Collections.Generic;
using System.Text;

namespace Pan_s_Room.Models
{
    public class Disc
    {
        public string Name { get; set; }
        public Artist Artist { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set; }
        public List<string> Tracklist { get; set; }
    }
}
