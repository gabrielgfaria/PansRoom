using System;
using System.Collections.Generic;
using System.Text;
using Pan_s_Room.Models;

namespace Pan_s_Room.Service
{
    public interface IDiscOperations
    {
        List<Disc> GetDiscs();
        List<Disc> GetDiscsByDiscName(string discName);
        List<Disc> GetDiscsByAuthor(string author);
        Disc AddDisc(Disc disc);
    }
}
