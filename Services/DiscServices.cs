using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Repository;

namespace Services
{
    public class DiscServices : CollectionServices<IDiscRepository>
    {
        private IDiscRepository _discRepository;

        public DiscServices(IDiscRepository discRepository) 
            : base(discRepository)
        {
            _discRepository = discRepository;
        }
    }
}
