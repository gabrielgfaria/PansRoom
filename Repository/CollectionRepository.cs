using Models;
using Repository.Context;

namespace Repository
{
    public class CollectionRepository : EntityRepository<Collection>
    {
        public CollectionRepository(PansRoomContext context)
            : base(context)
        {
        }
    }
}
