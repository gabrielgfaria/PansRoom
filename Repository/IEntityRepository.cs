using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IEntityRepository<TEntity> where TEntity : class, new()
    {
        public TEntity Add(TEntity entity);
        
        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        public IEnumerable<TEntity> FindAll();

        public IEnumerable<TEntity> FindAllIncludingNestedProps(
            params string[] propertiesToInclude);

        public void Remove(TEntity entity);

        public void RemoveRange(IEnumerable<TEntity> entities);
    }
}
