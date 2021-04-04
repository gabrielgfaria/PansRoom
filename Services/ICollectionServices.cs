using System.Collections.Generic;
using Models;

namespace Services
{
    public interface ICollectionServices
    {
        Disc AddDisc(Disc disc);
        Disc AddDiscAnyways(Disc disc);
        List<Disc> GetDiscs();
        void SaveDisc(Disc disc);
        void RemoveDisc(Disc disc);
    }
}