using System;
using System.Collections.Generic;
using System.Text;
using Pan_s_Room.Models;

namespace Pan_s_Room.Repository
{
    public interface IDiscRepository
    {
        List<Disc> GetDiscs();
        void SaveDiscs(List<Disc> discs);
    }
}
