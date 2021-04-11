using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repository;

namespace Services
{
    public abstract class DiscServices<TEntity> : IDiscServices<TEntity>
    {
        private readonly IEntityRepository<Artist> _artistRepository;
        public DiscServices(IEntityRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public abstract Disc AddDisc(Disc disc);

        public abstract Disc AddDiscAnyways(Disc disc);

        public abstract List<TEntity> GetDiscs();

        public abstract void RemoveDisc(Disc disc);

        protected void SetArtist(Disc disc)
        {
            var artists = _artistRepository.FindAll();
            var existingArtist = artists.SingleOrDefault(a => a.Name.ToLower() == disc.Artist.Name.ToLower());
            if (existingArtist != null)
            {
                disc.Artist = null;
                disc.ArtistId = existingArtist.Id;
            }
        }
    }
}
