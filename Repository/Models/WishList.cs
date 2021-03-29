using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class WishList
    {
        public int DiscId { get; set; }
        public bool AlreadyInCollection { get; set; }

        public virtual Disc Disc { get; set; }
    }
}
