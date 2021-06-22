using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Domain.Repository.Shared
{
    internal class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static readonly Object _lockObject = new object();

        public IUnitOfWork Create()
        {
            DbContext context;

            lock (_lockObject)
            {
                context = new IssueListContext();
            }

            return new UnitOfWorkEF(context);
        }
    }
}

