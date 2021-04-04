using System;
using System.Collections.Generic;
using System.Linq;
using Logger;
using Models;
using Repository;

namespace Services
{
    public class CollectionServices : ICollectionServices
    {
        private IEntityRepository<Disc> _discRepository;
        private IEntityRepository<WishList> _wishListRepository;
        private IEntityRepository<Collection> _collectionRepository;
        private IEntityRepository<Artist> _artistRepository;
        private IWishListServices _wishListServices;
        private ILogger _logger;

        public CollectionServices(IWishListServices wishListServices,
            IEntityRepository<Disc> discRepository,
            IEntityRepository<WishList> wishListRepository,
            ILogger logger,
            IEntityRepository<Collection> collectionRepository,
            IEntityRepository<Artist> artistRepository)
        {
            _wishListServices = wishListServices;
            _discRepository = discRepository;
            _wishListRepository = wishListRepository;
            _collectionRepository = collectionRepository;
            _logger = logger;
            _artistRepository = artistRepository;
        }

        public Disc AddDisc(Disc disc)
        {
            var existingDiscs = GetDiscs();
            var wishList = _wishListServices.GetDiscs();

            if (existingDiscs.Any(d => d.Name.ToLower() == disc.Name.ToLower() &&
            d.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
            {
                throw new ExistingDiscInCollectionException();
            }
            else if (wishList.Select(d => d.Disc).Any(d => d.Name.ToLower() == disc.Name.ToLower() &&
                d.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
            {
                var discToRemove = wishList.Where(d => d.Disc.Name.ToLower() == disc.Name.ToLower() &&
                d.Disc.Artist.Name.ToLower() == disc.Artist.Name.ToLower()).FirstOrDefault();
                _wishListRepository.Remove(discToRemove);
            }
            var artists = _artistRepository.FindAll();
            var existingArtist = artists.SingleOrDefault(a => a.Name.ToLower() == disc.Artist.Name.ToLower());
            if(existingArtist != null)
            {
                disc.Artist = null;
                disc.ArtistId = existingArtist.Id;
            }
            SaveDisc(disc);
            _logger.SetLogMessage("The disc was successfully added to your collection");

            return disc;
        }

        public Disc AddDiscAnyways(Disc disc)
        {
            var wishList = _wishListRepository.FindAll();
            if (wishList.Select(d => d.Disc).Any(d => d.Name.ToLower() == disc.Name.ToLower() &&
                d.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
            {
                var discToRemove = wishList.Where(d => d.Disc.Name.ToLower() == disc.Name.ToLower() &&
                d.Disc.Artist.Name.ToLower() == disc.Artist.Name.ToLower()).FirstOrDefault();
                _wishListRepository.Remove(discToRemove);
            }
            var addedDisc = _collectionRepository.Add(new Collection() { Disc = disc });
            _logger.SetLogMessage("The disc was successfully added to your collection");

            return addedDisc.Disc;
        }

        public List<Disc> GetDiscs()
            => _collectionRepository.FindAllIncludingNestedProps("Disc.Artist").Select(d => d.Disc).ToList();

        public void RemoveDisc(Disc disc)
        {
            var discToRemove = _discRepository.FindAll().Where(d => d == disc).FirstOrDefault();
            _discRepository.Remove(discToRemove);
        }

        public void SaveDisc(Disc disc)
        {
            var newItem = new Collection() { Disc = disc };
            _collectionRepository.Add(newItem);
        }
    }
}
