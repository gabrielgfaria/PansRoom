using System.Collections.Generic;
using Models;

namespace Services
{
    public interface IWishListServices : IServiceBase
    {
        List<Disc> GetDiscsFromRepository();
        void SaveDiscs(List<Disc> discs);
    }
}