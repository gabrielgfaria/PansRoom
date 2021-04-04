using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public partial class Artist
    {
        public Artist()
        {
            Discs = new HashSet<Disc>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Disc> Discs { get; set; }
    }
}
