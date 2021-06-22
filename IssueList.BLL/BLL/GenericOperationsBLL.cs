using IssueList.Domain.Model;
using IssueList.Domain.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.BLL.BLL
{
    public class GenericOperationsBLL<T> : ICrud<T> where T : Entity<int>
    {
        protected IRepositorio<T> repository = null;
        
        private bool _automaticCommit = true;

        public void SetAutomaticCommit(bool value)
        {
            _automaticCommit = value;
        }

        public GenericOperationsBLL(IUnitOfWork uow) : this(new Repository<T>(uow))
        {
        }

        public GenericOperationsBLL(IRepositorio<T> repository)
        {
            this.repository = repository;
        }


        /// <summary>
        /// Obtain entity
        /// </summary>
        /// <param name="id">Entity Id.</param>
        /// <returns>Entity</returns>
        public virtual T Obtain(object id) => repository.Obtain(id);

      

        /// <summary>
        /// Insert new entity
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        public virtual void Create(T entity)
        {
            repository.Insert(entity);
            AutomaticCommit();
        }

        /// <summary>
        /// Updates entity.
        /// </summary>
        /// <param name="entity">Entity to update</param>
        public virtual void Update(T entity)
        {
            repository.Update(entity);
            AutomaticCommit();
        }

        /// <summary>
        /// Delete Entity.
        /// </summary>
        /// <param name="id">ID </param>
        public virtual void Delete(object id)
        {
            repository.Delete(id);
            AutomaticCommit();
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(T entity)
        {
            repository.Delete(entity);
            AutomaticCommit();
        }



        /// <summary>
        /// Save changes automatic
        /// </summary>
        /// <param name="automaticCheck">Commit if the automatic commit is active, if not, do not commit as it will be part of a transaction.</param>
        public virtual void Commit(bool automaticCheck = false)
        {
            if (!automaticCheck || _automaticCommit)
                repository.SaveChanges();
        }

        /// <summary>
        /// Save changes automatic
        /// </summary>
        private void AutomaticCommit()
        {
            if (this._automaticCommit)
                repository.SaveChanges();
        }



        /// <summary>
        /// Method that inserts or modifies an entity
        /// </summary>
        /// <remarks>if id != 0 update, else inserts entity </remarks>
        /// <param name="entity"></param>
        public void InsertUpdate(T entity)
        {
            if (entity.Id != 0)
                Update(entity);
            else
                Create(entity);

        }

        /// <summary>
        /// Executes sql command
        /// </summary>
        /// <param name="sql">Sql string command</param>
        protected void ExecuteSqlCommand(string sql)
        {
            repository.ExecuteSqlCommand(sql);
        }
    }
}
