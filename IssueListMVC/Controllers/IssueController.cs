using IssueList.BLL.BLL;
using IssueList.BLL.DTO;
using IssueList.Domain;
using IssueList.Domain.Model;
using IssueList.Domain.Repository;
using IssueListMVC.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IssueListMVC.Controllers
{
    [Authorize]
    public class IssueController : BaseCtrl
    {
        private readonly IssueBLL _issueBLL;
        public IssueController() : base()
        {
            _issueBLL = new IssueBLL(uow);
        }

        public IEnumerable<IssueModel> Get()
        {
             return  _issueBLL.getAll().ToList();
        }

        public void Post([FromBody]Issue value)
        {
            _issueBLL.Create(value);

            refreshClients(_issueBLL.getAll());
        }

        public void Put(int id, [FromBody]Issue value)
        {
            _issueBLL.Update(value);

            refreshClients(_issueBLL.getAll());
        }

        public void Delete(int id)
        {
            _issueBLL.Delete(id);

            refreshClients(_issueBLL.getAll());
        }
      
    }
}
