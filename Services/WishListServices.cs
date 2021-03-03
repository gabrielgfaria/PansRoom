using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using Repository;

namespace Services
{
    public class WishListServices : ServiceBase, IWishListServices
    {
        private ICollectionRepository<WishList> _wishListRepository;
        private ICollectionRepository<Disc> _discRepository;

        public WishListServices(ICollectionRepository<WishList> wishListRepository,
            ICollectionRepository<Disc> discRepository)
        {
            _wishListRepository = wishListRepository;
            _discRepository = discRepository;
        }

        public override Disc AddDisc(Disc disc)
        {
            var existingDiscs = GetDiscsFromCollection();
            var wishListDiscs = GetDiscsFromRepository();
            if (existingDiscs.Any(d => d.Name.ToLower() == disc.Name.ToLower() &&
                d.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
            {
                throw new ExistingWishListItemInCollectionException();
            }
            wishListDiscs.Add(disc);
            SaveDiscs(wishListDiscs);

            return disc;
        }

        public override List<Disc> GetDiscsFromRepository()
        {
            return _wishListRepository.GetDiscs();
        }
        
        private List<Disc> GetDiscsFromCollection()
        {
            return _discRepository.GetDiscs();
        }

        public override void SaveDiscs(List<Disc> discs)
        {
            _wishListRepository.SaveDiscs(discs);
        }
    }
}
