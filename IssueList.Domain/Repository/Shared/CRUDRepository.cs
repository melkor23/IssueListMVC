using IssueList.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Domain.Repository.Shared
{
    public class CRUDRepository<T> : BaseRepository, IRepositorio<T>
         where T : class
    {
        DbSet<T> _dbSet;
        protected DbSet<T> DbSet
        {
            get
            {
                if (_dbSet == null)
                    _dbSet = Context.Set<T>();
                return _dbSet;
            }
        }

        public CRUDRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public T Obtain(params object[] id)
        {
            return DbSet.Find(id);
        }


        public T Obtain(T entity)
        {
            return DbSet.Find(GetKeyValues(entity));
        }


        public IQueryable<T> ObtainQueryable(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                    query = query.Include(includeProperty);
            }

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return orderBy(query);
            else
                return query;
        }


        public void Insert(T entity)
        {
            OnInsert(entity);
            DbSet.Add(entity);
        }


        protected virtual void OnInsert(T entity) { }


        public void Update(T entity)
        {
            T dbEntity = Obtain(entity);

            OnUpdate(dbEntity, entity);
        }

      
        protected virtual void OnUpdate(T dbEntity, T origin)
        {
            Context.Entry(dbEntity).State = EntityState.Detached;

            DbSet.Attach(origin);
            Context.Entry(origin).State = EntityState.Modified;
        }

        public void Delete(params object[] id)
        {
            var dbEntity = Obtain(id);


            DeleteDBEntity(dbEntity);
        }

        public void Delete(T entity)
        {
            var dbEntity = Obtain(entity);

            DeleteDBEntity(dbEntity);
        }

        
        private void DeleteDBEntity(T dbEntity)
        {
            OnDelete(dbEntity);
            DbSet.Remove(dbEntity);
        }

       
        protected virtual void OnDelete(T dbEntity) { }

      
        public void SaveChanges()
        {
            UoW.Commit();
        }

        public int ExecuteSqlCommand(string sql)
        {
            return Context.Database.ExecuteSqlCommand(sql);
        }

    }
}
