using IssueList.BLL.DTO;
using IssueList.Domain.Model;
using IssueList.Domain.Repository;
using IssueList.Domain.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.BLL.BLL
{
    public class IssueBLL : GenericOperationsBLL<Issue>
    {
        private readonly IssueRepository _repositorio;

        public IssueBLL(IUnitOfWork uow) : this(new IssueRepository(uow)) { }

        public IssueBLL(IssueRepository repository) : base(repository)
        {
            _repositorio = repository;
        }

        /// <summary>
        /// Get All Issues
        /// </summary>
        public IEnumerable<IssueModel> getAll()
        {
            return _repositorio.getAll().Select(IssueModel.ToModelConverterExpression).ToList();
        }
    }
}
