using System.Collections.Generic;
using Models;

namespace Services
{
    public interface IDiscServices : IServiceBase
    {
        List<Disc> GetDiscsFromRepository();
        void SaveDiscs(List<Disc> discs);
    }
}