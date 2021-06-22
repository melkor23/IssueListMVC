using Fleck;
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
    public class WebSocketController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<WebSocketConnectionModel> Get()
        {
            IQueryable<IWebSocketConnection> query = IssuelistServer.getSockets().AsQueryable();
            var aux=query.Select(WebSocketConnectionModel.ToModelConverterExpression).ToList();

            return aux;
        }
    }
}