using System.Collections.Generic;
using Models;

namespace Services
{
    public interface IDiscServices
    {
        Disc AddDisc(Disc disc);
        List<Disc> GetDiscs();
        List<Disc> GetDiscsByAuthor(string author);
        List<Disc> GetDiscsByDiscName(string discName);
    }
}