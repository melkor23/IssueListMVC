using IssueList.Domain.Model;
using IssueList.Domain.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Domain.Repository
{
    public class IssueRepository :  Repository<Issue>
    {

        public IssueRepository(IUnitOfWork uow) : base(uow)
        { }

        /// <summary>
        /// Gets all issues
        /// </summary>
        /// <returns>Issue queryable </returns>
        public IQueryable<Issue> getAll()
            => ObtainQueryable();
    }

}
