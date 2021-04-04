using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Collection
    {
        public int Id { get; set; }
        public int DiscId { get; set; }

        public virtual Disc Disc { get; set; }
    }
}
