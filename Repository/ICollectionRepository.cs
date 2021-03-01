using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Repository
{
    public interface ICollectionRepository
    {
        List<Disc> GetDiscs();
        void SaveDiscs(List<Disc> discs);
    }
}
