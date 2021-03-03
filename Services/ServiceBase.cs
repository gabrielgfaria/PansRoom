using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using Repository;

namespace Services
{
    public abstract class ServiceBase : IServiceBase
    {
        public ServiceBase()
        {
        }

        public virtual Disc AddDiscAnyways(Disc disc)
        {
            var existingDiscs = GetDiscsFromRepository();
            existingDiscs.Add(disc);

            SaveDiscs(existingDiscs);
            return disc;
        }

        public virtual List<Disc> GetDiscs()
        {
            var existingDiscs = GetDiscsFromRepository();

            return existingDiscs;
        }

        public virtual List<Disc> GetDiscsByAuthor(string author)
        {
            var existingDiscs = GetDiscsFromRepository();

            return existingDiscs.Where(d => d.Artist.Name.ToLower().Equals(author.ToLower())).ToList();
        }

        public virtual List<Disc> GetDiscsByDiscName(string discName)
        {
            var existingDiscs = GetDiscsFromRepository();

            return existingDiscs.Where(d => d.Name.ToLower().Equals(discName.ToLower())).ToList();
        }

        public virtual void RemoveDisc(Disc disc)
        {
            var existingDiscs = GetDiscsFromRepository();
            existingDiscs.Remove(existingDiscs.Single(d => d.Name == disc.Name && d.Artist.Name == disc.Artist.Name));

            SaveDiscs(existingDiscs);
        }

        public abstract Disc AddDisc(Disc disc);

        public abstract List<Disc> GetDiscsFromRepository();
        
        public abstract void SaveDiscs(List<Disc> discs);
    }
}
