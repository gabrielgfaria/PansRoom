using Models;
using Repository.Context;

namespace Repository
{
    public class DiscRepository : EntityRepository<Disc>
    {
        public DiscRepository(PansRoomContext context)
            : base(context)
        {
        }
    }
}
