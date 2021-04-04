using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logger;
using Models;
using Repository;

namespace Services
{
    public class WishListServices : IWishListServices
    {
        private IEntityRepository<WishList> _wishListRepository;
        private IEntityRepository<Disc> _discRepository;
        private ILogger _logger;

        public WishListServices(IEntityRepository<WishList> wishListRepository,
            IEntityRepository<Disc> discRepository,
            ILogger logger)
        {
            _wishListRepository = wishListRepository;
            _discRepository = discRepository;
            _logger = logger;
        }

        public Disc AddDisc(Disc disc)
        {
            var existingDiscs = GetDiscsFromCollection();
            if (existingDiscs.Any(d => d.Name.ToLower() == disc.Name.ToLower() &&
                d.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
            {
                throw new ExistingWishListItemInCollectionException();
            }
            SaveDisc(new WishList() { Disc = disc, AlreadyInCollection = false });
            _logger.SetLogMessage("The disc was successfully added to your wishlist");

            return disc;
        }

        public Disc AddDiscAnyways(Disc disc)
        {

            var addedDisc = _wishListRepository.Add(new WishList() { Disc = disc });
            _logger.SetLogMessage("The disc was successfully added to your wishlist");

            return addedDisc.Disc;
        }

        public void RemoveDisc(Disc disc)
        {
            var discToBeRemoved = _wishListRepository.FindAll().Where(d => d.Disc == disc).FirstOrDefault();
            _wishListRepository.Remove(discToBeRemoved);
        }

        public List<WishList> GetDiscs()
            => _wishListRepository.FindAllIncludingNestedProps("Disc.Artist").ToList();

        private List<Disc> GetDiscsFromCollection()
        {
            return _discRepository.FindAll().ToList();
        }

        public void SaveDisc(WishList disc)
        {
            _wishListRepository.Add(disc);
        }
    }
}
