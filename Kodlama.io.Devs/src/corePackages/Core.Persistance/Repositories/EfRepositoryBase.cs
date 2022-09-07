using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories
{
    public class EfRepositoryBase<TEntity, TContext> : IAsyncRepository<TEntity>, IRepository<TEntity>
        where TEntity : Entity
        where TContext : DbContext
    {
        protected TContext Context { get; }

        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(x=>x.IsActive).FirstOrDefault(predicate);
        }

        public TEntity Add(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.IsActive = true;
            Context.Entry(entity).State = EntityState.Added;
            Context.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.SaveChanges();
            return entity;
        }

        public IPaginate<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0, int size = 10,
            bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if(include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            
            if (orderBy != null) return orderBy(queryable).ToPaginate(index, size);
            return queryable.Where(x=>x.IsActive).ToPaginate(index, size);
        }

        public IPaginate<TEntity> GetListByDynamic(Dynamic.Dynamic dynamic,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0, int size = 10,
            bool enableTracking = true)
        {
            IQueryable<TEntity> quaryable = Query().AsQueryable().ToDynamic(dynamic);
            if (!enableTracking) quaryable = quaryable.AsNoTracking();
            if(include != null) quaryable = include(quaryable);
            return quaryable.Where(x => x.IsActive).ToPaginate(index, size);
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>().Where(x => x.IsActive);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(x => x.IsActive).AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.IsActive = true;
            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0, int size = 10, bool enableTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            
            if (orderBy != null)
                return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
            return await queryable.Where(x => x.IsActive).ToPaginateAsync(index, size, 0, cancellationToken);
        }

        public async Task<IPaginate<TEntity>> GetListByDynamicAsync(Dynamic.Dynamic dynamic, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0, int size = 10, bool enableTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query().AsQueryable().ToDynamic(dynamic);
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return await queryable.Where(x => x.IsActive).ToPaginateAsync(index, size, 0, cancellationToken);
        }

        public TEntity SoftDelete(TEntity entity)
        {
            var deletedEntity = Context.Set<TEntity>().Find(entity.Id);
            deletedEntity.GetType().GetProperty("IsActive").SetValue(deletedEntity, false);
            return Update(deletedEntity);
        }

        public async Task<TEntity> SoftDeleteAsync(TEntity entity)
        {
            var deletedEntity = await Context.Set<TEntity>().FindAsync(entity.Id);
            deletedEntity.GetType().GetProperty("IsActive").SetValue(deletedEntity, false);
            return await UpdateAsync(deletedEntity);
        }

        
    }
}
