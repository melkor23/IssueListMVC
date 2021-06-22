using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.BLL.DTO
{
    public class WebSocketConnectionModel : ModelBase<IWebSocketConnection, WebSocketConnectionModel>
    {
        public string clientId { get; set; }
        public string remoteIpAddress { get; set; }
        public int remotePort { get; set; }
        

        protected override Expression<Func<IWebSocketConnection, WebSocketConnectionModel>> ProyeccionExpression => ExpressionSelector;

        public static Expression<Func<IWebSocketConnection, WebSocketConnectionModel>> ToModelConverterExpression
            => ExpressionSelector;                

        public static readonly Expression<Func<IWebSocketConnection, WebSocketConnectionModel>> ExpressionSelector = ws =>
            new WebSocketConnectionModel
            {
                remoteIpAddress = ws.ConnectionInfo.ClientIpAddress,
                clientId = ws.ConnectionInfo.Id.ToString(),
                remotePort = ws.ConnectionInfo.ClientPort
            };

        

        public override IWebSocketConnection ToEF() => throw new NotImplementedException();
    }
}
