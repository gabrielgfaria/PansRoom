using System;
using System.Collections.Generic;
using System.Linq;
using Logger;
using Models;
using Repository;

namespace Services
{
    public class CollectionServices : IDiscServices<Collection>
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
            var wishList = _wishListRepository.FindAllIncludingNestedProps("Disc.Artist").ToList();

            if (existingDiscs.Any(d => d.Disc.Name.ToLower() == disc.Name.ToLower() &&
            d.Disc.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
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
            SaveDisc(new Collection() { Disc = disc });
            _logger.SetLogMessage("The disc was successfully added to your collection");

            return disc;
        }

        public Disc AddDiscAnyways(Disc disc)
        {
            var wishList = _wishListRepository.FindAllIncludingNestedProps("Disc.Artist").ToList();
            if (wishList.Select(d => d.Disc).Any(d => d.Name.ToLower() == disc.Name.ToLower() &&
                d.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
            {
                var discToRemove = wishList.Where(d => d.Disc.Name.ToLower() == disc.Name.ToLower() &&
                d.Disc.Artist.Name.ToLower() == disc.Artist.Name.ToLower()).FirstOrDefault();
                _wishListRepository.Remove(discToRemove);
            }
            var artists = _artistRepository.FindAll();
            var existingArtist = artists.SingleOrDefault(a => a.Name.ToLower() == disc.Artist.Name.ToLower());
            if (existingArtist != null)
            {
                disc.Artist = null;
                disc.ArtistId = existingArtist.Id;
            }
            var addedDisc = _collectionRepository.Add(new Collection() { Disc = disc });
            _logger.SetLogMessage("The disc was successfully added to your collection");

            return addedDisc.Disc;
        }

        public List<Collection> GetDiscs()
            => _collectionRepository.FindAllIncludingNestedProps("Disc.Artist")
            .ToList();

        public void RemoveDisc(Disc disc)
        {
            var itemToRemove = _collectionRepository.FindAllIncludingNestedProps("Disc.Artist")
                .Where(d => d.Disc.Artist.Name == disc.Artist.Name && d.Disc.Name == disc.Name);
            _collectionRepository.RemoveRange(itemToRemove);
            _discRepository.RemoveRange(itemToRemove.Select(d => d.Disc));
            _logger.SetLogMessage("The disc(s) was(were) successfully removed from your collection");
        }

        private  void SaveDisc(Collection disc)
        {
            _collectionRepository.Add(disc);
        }
    }
}
