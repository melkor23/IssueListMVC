using IssueList.BLL.BLL;
using IssueList.BLL.DTO;
using IssueList.Domain;
using IssueList.Domain.Repository.Shared;
using IssueListMVC.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace IssueListMVC.Controllers
{
    public class BaseCtrl : ApiController
    {
        internal IUnitOfWork uow;

        public BaseCtrl() : base()
        {
            uow = ManagerDataContext.GetUow();
        }

        protected void refreshClients(IEnumerable<IssueModel> IssueList )
        {
            IssuelistServer.refreshClientsIssueList(IssueList);
        }
    }
}