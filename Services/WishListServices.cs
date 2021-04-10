﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logger;
using Models;
using Repository;

namespace Services
{
    public class WishListServices : IDiscServices<WishList>
    {
        private IEntityRepository<WishList> _wishListRepository;
        private IEntityRepository<Disc> _discRepository;
        private IEntityRepository<Artist> _artistRepository;
        private IEntityRepository<Collection> _collectionRepository;
        private ILogger _logger;

        public WishListServices(IEntityRepository<WishList> wishListRepository,
            IEntityRepository<Disc> discRepository,
            ILogger logger,
            IEntityRepository<Artist> artistRepository,
            IEntityRepository<Collection> collectionRepository)
        {
            _wishListRepository = wishListRepository;
            _discRepository = discRepository;
            _logger = logger;
            _artistRepository = artistRepository;
            _collectionRepository = collectionRepository;
        }

        public Disc AddDisc(Disc disc)
        {
            var existingDiscs = GetDiscsFromCollection();
            if (existingDiscs.Any(d => d.Name.ToLower() == disc.Name.ToLower() &&
                d.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
            {
                throw new ExistingWishListItemInCollectionException();
            }
            var artists = _artistRepository.FindAll();
            var existingArtist = artists.SingleOrDefault(a => a.Name.ToLower() == disc.Artist.Name.ToLower());
            if (existingArtist != null)
            {
                disc.Artist = null;
                disc.ArtistId = existingArtist.Id;
            }
            SaveDisc(disc);
            _logger.SetLogMessage("The disc was successfully added to your wishlist");

            return disc;
        }

        public Disc AddDiscAnyways(Disc disc)
        {
            var artists = _artistRepository.FindAll();
            var existingArtist = artists.SingleOrDefault(a => a.Name.ToLower() == disc.Artist.Name.ToLower());
            if (existingArtist != null)
            {
                disc.Artist = null;
                disc.ArtistId = existingArtist.Id;
            }
            var addedDisc = _wishListRepository.Add(new WishList() { Disc = disc, AlreadyInCollection = true });
            _logger.SetLogMessage("The disc was successfully added to your wishlist");

            return addedDisc.Disc;
        }

        public void RemoveDisc(Disc disc)
        {
            var itemToRemove = _wishListRepository.FindAllIncludingNestedProps("Disc.Artist")
                .Where(d => d.Disc.Artist.Name == disc.Artist.Name && d.Disc.Name == disc.Name);
            _wishListRepository.RemoveRange(itemToRemove);
            _discRepository.RemoveRange(itemToRemove.Select(d => d.Disc));
            _logger.SetLogMessage("The disc(s) was(were) successfully removed from your wishlist");
        }

        private List<Disc> GetDiscsFromCollection()
            => _collectionRepository.FindAllIncludingNestedProps("Disc.Artist")
            .Select(d => d.Disc)
            .ToList();

        public List<WishList> GetDiscs()
            => _wishListRepository.FindAllIncludingNestedProps("Disc.Artist")
            .ToList();

        public void SaveDisc(Disc disc)
            => _wishListRepository.Add(new WishList() { Disc = disc, AlreadyInCollection = false });
    }
}
