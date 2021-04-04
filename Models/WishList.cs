using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public partial class WishList
    {
        public int Id { get; set; }
        public int DiscId { get; set; }
        public bool AlreadyInCollection { get; set; }

        public virtual Disc Disc { get; set; }
    }
}
