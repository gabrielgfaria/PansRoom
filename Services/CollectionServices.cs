using System;
using System.Collections.Generic;
using System.Linq;
using Logger;
using Models;
using Repository;

namespace Services
{
    public class CollectionServices : DiscServices<Collection>
    {
        private IEntityRepository<Disc> _discRepository;
        private IEntityRepository<WishList> _wishListRepository;
        private IEntityRepository<Collection> _collectionRepository;
        private IEntityRepository<Artist> _artistRepository;
        private IDiscServices<WishList> _wishListServices;
        private ILogger _logger;

        public CollectionServices(IDiscServices<WishList> wishListServices,
            IEntityRepository<Disc> discRepository,
            IEntityRepository<WishList> wishListRepository,
            ILogger logger,
            IEntityRepository<Collection> collectionRepository,
            IEntityRepository<Artist> artistRepository)
            : base(artistRepository)
        {
            _wishListServices = wishListServices;
            _discRepository = discRepository;
            _wishListRepository = wishListRepository;
            _collectionRepository = collectionRepository;
            _logger = logger;
            _artistRepository = artistRepository;
        }

        public override Disc AddDisc(Disc disc)
        {
            if(DiscExistsInCollection(disc))
                throw new ExistingDiscInCollectionException();

            var discInWishlist = DiscInWishList(disc);
            if (discInWishlist != null)
                RemoveDiscFromWishlist(discInWishlist);

            SetArtist(disc);

            SaveDisc(new Collection() { Disc = disc });
            
            _logger.SetLogMessage("The disc was successfully added to your collection");

            return disc;
        }

        public override Disc AddDiscAnyways(Disc disc)
        {
            var discInWishlist = DiscInWishList(disc);
            if (discInWishlist != null)
                RemoveDiscFromWishlist(discInWishlist);

            SetArtist(disc);

            SaveDisc(new Collection() { Disc = disc });
            
            _logger.SetLogMessage("The disc was successfully added to your collection");

            return disc;
        }

        public override List<Collection> GetDiscs()
            => _collectionRepository.FindAllIncludingNestedProps("Disc.Artist")
            .ToList();

        public override void RemoveDisc(Disc disc)
        {
            var itemToRemove = GetDiscs()
                .Where(d => d.Disc.Artist.Name == disc.Artist.Name && d.Disc.Name == disc.Name);
            
            _collectionRepository.RemoveRange(itemToRemove);
            _discRepository.RemoveRange(itemToRemove.Select(d => d.Disc));
            _logger.SetLogMessage("The disc(s) was(were) successfully removed from your collection");
        }

        
        private  void SaveDisc(Collection disc)
        {
            _collectionRepository.Add(disc);
        }

        private void RemoveDiscFromWishlist(WishList disc)
        {
            _wishListRepository.Remove(disc);
        }

        private WishList DiscInWishList(Disc disc)
        {
            var wishList = _wishListServices.GetDiscs();

            return wishList.Where(d => d.Disc.Name.ToLower() == disc.Name.ToLower() &&
                d.Disc.Artist.Name.ToLower() == disc.Artist.Name.ToLower()).FirstOrDefault();
        }

        private bool DiscExistsInCollection(Disc disc)
        {
            var existingDiscs = GetDiscs();
            if (existingDiscs.Any(d => d.Disc.Name.ToLower() == disc.Name.ToLower() &&
            d.Disc.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
                return true;

            return false;
        }
    }
}
