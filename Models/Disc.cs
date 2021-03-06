﻿using System.Collections.Generic;

namespace Models
{
    public partial class Disc
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArtistId { get; set; }
        public int Year { get; set; }

        public virtual Artist Artist { get; set; }
    }
}
