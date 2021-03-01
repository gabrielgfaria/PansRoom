using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Repository;

namespace Services
{
    public class DiscServices : CollectionServices<ICollectionRepository<Disc>>
    {
        private ICollectionRepository<Disc> _discRepository;

        public DiscServices(ICollectionRepository<Disc> discRepository) 
            : base(discRepository)
        {
            _discRepository = discRepository;
        }
    }
}
