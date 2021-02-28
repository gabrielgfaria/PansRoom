using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Repository;

namespace Services
{
    public class DiscServices : IDiscServices
    {
        private IDiscRepository _discRepository;

        public DiscServices(IDiscRepository discRepository)
        {
            _discRepository = discRepository;
        }

        public Disc AddDisc(Disc disc)
        {
            var existingDiscs = _discRepository.GetDiscs();
            existingDiscs.Add(disc);

            _discRepository.SaveDiscs(existingDiscs);
            return disc;
        }

        public List<Disc> GetDiscs()
        {
            var existingDiscs = _discRepository.GetDiscs();

            return existingDiscs;
        }

        public List<Disc> GetDiscsByAuthor(string author)
        {
            var existingDiscs = _discRepository.GetDiscs();

            return existingDiscs.Where(d => d.Artist.Name.ToLower().Equals(author.ToLower())).ToList();
        }

        public List<Disc> GetDiscsByDiscName(string discName)
        {
            var existingDiscs = _discRepository.GetDiscs();

            return existingDiscs.Where(d => d.Name.ToLower().Equals(discName.ToLower())).ToList();
        }
    }
}
