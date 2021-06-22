using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Domain.Repository.Shared
{
    internal class UnitOfWorkEF : IUnitOfWork
    {
        public DbContext Context { get; private set; }

        public UnitOfWorkEF(DbContext context)
        {
            Context = context;
        }

        object IUnitOfWork.Context
        {
            get
            {
                return Context;
            }
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }

            GC.SuppressFinalize(this);
        }

    }
}
