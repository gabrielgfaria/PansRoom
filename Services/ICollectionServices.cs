using System.Collections.Generic;
using Models;
using Repository;

namespace Services
{
    public interface ICollectionServices<T> where T : ICollectionRepository
    {
        Disc AddDisc(Disc disc);
        List<Disc> GetDiscs();
        List<Disc> GetDiscsByAuthor(string author);
        List<Disc> GetDiscsByDiscName(string discName);
    }
}