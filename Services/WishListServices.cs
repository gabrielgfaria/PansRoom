﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logger;
using Models;
using Repository;

namespace Services
{
    public class WishListServices : DiscServices<WishList>
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
            : base (artistRepository)
        {
            _wishListRepository = wishListRepository;
            _discRepository = discRepository;
            _logger = logger;
            _artistRepository = artistRepository;
            _collectionRepository = collectionRepository;
        }

        public override Disc AddDisc(Disc disc)
        {
            if (DiscExistsInCollection(disc))
                throw new ExistingWishListItemInCollectionException();

            SetArtist(disc);
            
            SaveDisc(new WishList() { Disc = disc, AlreadyInCollection = false });
            _logger.SetLogMessage("The disc was successfully added to your wishlist");

            return disc;
        }

        public override Disc AddDiscAnyways(Disc disc)
        {
            SetArtist(disc);

            SaveDisc(new WishList() { Disc = disc, AlreadyInCollection = true });

            _logger.SetLogMessage("The disc was successfully added to your wishlist");

            return disc;
        }

        public override List<WishList> GetDiscs()
            => _wishListRepository.FindAllIncludingNestedProps("Disc.Artist")
            .ToList();
        
        public override void RemoveDisc(Disc disc)
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

        private void SaveDisc(WishList disc)
            => _wishListRepository.Add(disc);

        private bool DiscExistsInCollection(Disc disc)
        {
            var existingDiscs = GetDiscsFromCollection();
            if (existingDiscs.Any(d => d.Name.ToLower() == disc.Name.ToLower() &&
                d.Artist.Name.ToLower() == disc.Artist.Name.ToLower()))
                return true;

            return false;
        }
    }
}
