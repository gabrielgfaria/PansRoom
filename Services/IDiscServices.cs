using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IDiscServices<TEntity>
    {
        Disc AddDisc(Disc disc);
        Disc AddDiscAnyways(Disc disc);
        List<TEntity> GetDiscs();
        void RemoveDisc(Disc disc);
    }
}
