using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using Repository;

namespace Services
{
    public class CollectionServices<T> : ICollectionServices<T> 
        where T : ICollectionRepository
    {
        private T _repository;

        public CollectionServices(T repository)
        {
            _repository = repository;
        }

        public Disc AddDisc(Disc disc)
        {
            var existingDiscs = _repository.GetDiscs();
            existingDiscs.Add(disc);

            _repository.SaveDiscs(existingDiscs);
            return disc;
        }

        public List<Disc> GetDiscs()
        {
            var existingDiscs = _repository.GetDiscs();

            return existingDiscs;
        }

        public List<Disc> GetDiscsByAuthor(string author)
        {
            var existingDiscs = _repository.GetDiscs();

            return existingDiscs.Where(d => d.Artist.Name.ToLower().Equals(author.ToLower())).ToList();
        }

        public List<Disc> GetDiscsByDiscName(string discName)
        {
            var existingDiscs = _repository.GetDiscs();

            return existingDiscs.Where(d => d.Name.ToLower().Equals(discName.ToLower())).ToList();
        }
    }
}
