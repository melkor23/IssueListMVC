using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Domain.Repository.Shared
{
    /// <summary>
    /// Generic repository class
    /// </summary>
    /// <typeparam name="T">Entity of repository</typeparam>
    public class Repository<T> : BaseRepository, IRepositorio<T>
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

        public Repository(IUnitOfWork uow) : base(uow)
        {
        }

        /// <summary>
        /// Obtain entity by Id
        /// </summary>
        /// <param name="id">Entity ID </param>
        /// <returns>Entity.</returns>
        public T Obtain(params object[] id)
        {
            return DbSet.Find(id);
        }

        /// <summary>   
        /// Obtain entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Obtain(T entity)
        {
            return DbSet.Find(GetKeyValues(entity));
        }

        /// <summary>
        /// Gets a queryable of the defined entity, searching for certain parameters..
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="orderBy">Order.</param>
        /// <param name="includeProperties">Dependent entities to include string format.</param>
        /// <returns>Entity Enumerable</returns>
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

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Insert(T entity)
        {
            OnInsertar(entity);
            DbSet.Add(entity);
        }

        /// <summary>
        /// Insert entity collections
        /// </summary>
        /// <param name="entities">Enumerable entity</param>
        public void InsertColection(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                OnInsertar(entity);
            DbSet.AddRange(entities);
        }

        /// <summary>
        /// Calls before insertion
        /// </summary>
        /// <param name="entity"><typeparamref name="T"/> Insert entity</param>
        protected virtual void OnInsertar(T entity) { }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Update(T entity)
        {
            T dbEntity = Obtain(entity);

            OnModificar(dbEntity, entity);
        }

        /// <summary>
        /// Calls before update 
        /// </summary>
        /// <param name="dbEntity"><typeparamref name="T"/> new entity</param>
        /// <param name="origin"><typeparamref name="T"/> base entity</param>
        protected virtual void OnModificar(T dbEntity, T origin)
        {
            Context.Entry(dbEntity).State = EntityState.Detached;

            DbSet.Attach(origin);
            Context.Entry(origin).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="id">Entity id</param>
        public void Delete(params object[] id)
        {
            var dbEntity = Obtain(id);
            ThrowIfNull(dbEntity);

            EliminarDBEntity(dbEntity);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Delete(T entity)
        {
            var dbEntity = Obtain(entity);
            ThrowIfNull(dbEntity);

            EliminarDBEntity(dbEntity);
        }


        private void EliminarDBEntity(T dbEntity)
        {
            OnEliminar(dbEntity);
            DbSet.Remove(dbEntity);
        }

        /// <summary>
        /// Calls before delete
        /// </summary>
        /// <param name="dbEntity"><typeparamref name="T"/> Entity</param>
        protected virtual void OnEliminar(T dbEntity) { }


        /// <summary>
        /// Save Database changes
        /// </summary>
        public void SaveChanges()
        {
            UoW.Commit();
        }

        public int ExecuteSqlCommand(string sql)
        {
            return Context.Database.ExecuteSqlCommand(sql);
        }

        private void ThrowIfNull(T entity)
        {
            if (entity == null)
                throw new Exception("Entity Error");
        }
    }
}

