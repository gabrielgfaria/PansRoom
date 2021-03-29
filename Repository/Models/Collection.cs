using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Collection
    {
        public int DiscId { get; set; }

        public virtual Disc Disc { get; set; }
    }
}
