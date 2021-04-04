﻿using System.Collections.Generic;
using Models;

namespace Services
{
    public interface IWishListServices
    {
        Disc AddDisc(Disc disc);
        Disc AddDiscAnyways(Disc disc);
        List<WishList> GetDiscs();
        void RemoveDisc(Disc disc);
        void SaveDisc(WishList disc);
    }
}