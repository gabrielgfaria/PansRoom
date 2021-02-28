using System.Collections.Generic;
using Models;

namespace Repository
{
    public interface IDiscRepository
    {
        List<Disc> GetDiscs();
        void SaveDiscs(List<Disc> discs);
    }
}