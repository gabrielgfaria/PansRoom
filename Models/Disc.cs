using System.Collections.Generic;

namespace Models
{
    public class Disc
    {
        public string Name { get; set; }
        public Artist Artist { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public List<string> Tracklist { get; set; }
    }
}
