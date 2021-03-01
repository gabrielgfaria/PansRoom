using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Repository;

namespace Services
{
    public class WishListServices : CollectionServices<ICollectionRepository<WishList>>
    {
        private ICollectionRepository<WishList> _wishListRepository;

        public WishListServices(ICollectionRepository<WishList> wishListRepository)
            : base(wishListRepository)
        {
            _wishListRepository = wishListRepository;
        }
    }
}
