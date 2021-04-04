using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using Repository.Context;

namespace Repository
{
    public class WishListRepository : EntityRepository<WishList>
    {
        public WishListRepository(PansRoomContext context)
            : base(context)
        {
        }
    }
}
