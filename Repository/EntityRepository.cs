using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, new()
    {
        private PansRoomContext _context;

        public EntityRepository(PansRoomContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _context.AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public IEnumerable<TEntity> FindAll()
            => _context.Set<TEntity>().ToList();

        public IEnumerable<TEntity> FindAllIncludingNestedProps(
            params string[] propertiesToInclude)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            query = propertiesToInclude.Aggregate(query,
                (current, property) => current.Include(property));

            return query.ToList();
        }

        public void Remove(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
