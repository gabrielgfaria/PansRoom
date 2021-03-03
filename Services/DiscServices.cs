using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Repository;

namespace Services
{
    public class DiscServices : ServiceBase, IDiscServices
    {
        private IWishListServices _wishListServices;
        private ICollectionRepository<Disc> _discRepository;
        private ICollectionRepository<WishList> _wishListRepository;

        public DiscServices(IWishListServices wishListServices,
            ICollectionRepository<Disc> discRepository,
            ICollectionRepository<WishList> wishListRepository)
        {
            _wishListServices = wishListServices;
            _discRepository = discRepository;
            _wishListRepository = wishListRepository;
        }

        public override Disc AddDisc(Disc disc)
        {
            var existingDiscs = GetDiscsFromRepository();
            var wishList = _wishListRepository.GetDiscs();

            if (existingDiscs.Any(d => d.Name.ToLower() == disc.Name.ToLower() &&
            d.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
            {
                throw new ExistingDiscInCollectionException();
            }
            else if (wishList.Any(d => d.Name.ToLower() == disc.Name.ToLower() &&
                d.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
            {
                _wishListServices.RemoveDisc(disc);
            }
            existingDiscs.Add(disc);
            SaveDiscs(existingDiscs);

            return disc;
        }

        public override Disc AddDiscAnyways(Disc disc)
        {
            var wishList = _wishListRepository.GetDiscs();
            if (wishList.Any(d => d.Name.ToLower() == disc.Name.ToLower() &&
                d.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
            {
                _wishListServices.RemoveDisc(disc);
            }
            return base.AddDiscAnyways(disc);
        }

        public override List<Disc> GetDiscsFromRepository()
        {
            return _discRepository.GetDiscs();
        }

        public override void SaveDiscs(List<Disc> discs)
        {
            _discRepository.SaveDiscs(discs);
        }
    }
}
