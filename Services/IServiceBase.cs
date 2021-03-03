using System.Collections.Generic;
using Models;
using Repository;

namespace Services
{
    public interface IServiceBase
    {
        Disc AddDisc(Disc disc);
        Disc AddDiscAnyways(Disc disc);
        void RemoveDisc(Disc disc);
        List<Disc> GetDiscs();
        List<Disc> GetDiscsByAuthor(string author);
        List<Disc> GetDiscsByDiscName(string discName);
    }
}