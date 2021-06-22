using Fleck;
using IssueList.BLL.BLL;
using IssueList.BLL.DTO;
using IssueList.Domain;
using IssueList.Domain.Model;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueListMVC.WebSocket
{
    public static class IssuelistServer
    {
        static Logger  log = LogManager.GetCurrentClassLogger();

        private static List<IWebSocketConnection> _webSocketsList { get; set; }


        public static void init(string ip, int port)
        {
            IssueBLL issueBLL = new IssueBLL(ManagerDataContext.GetUow());

            string direccion = "ws://" + ip + ":" + port;
            log.Info("Server listen on "+direccion);

            _webSocketsList = new List<IWebSocketConnection>();
            var server = new WebSocketServer(direccion);

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    _webSocketsList.Add(socket);

                    log.Info("Conected ClientId: "+ socket.ConnectionInfo.Id);
                    socket.Send(JsonConvert.SerializeObject(issueBLL.getAll()));
                };

                socket.OnClose = () =>
                {   
                    _webSocketsList.Remove(socket);
                    log.Info("Disconnected ClientId: "+ socket.ConnectionInfo.Id);
                };

                socket.OnMessage = (message) =>
                {
                    log.Info("Client Id:" + socket.ConnectionInfo.Id +" send message" + message);
                    _webSocketsList.ForEach(s =>
                    {
                        if (socket.ConnectionInfo.Id != s.ConnectionInfo.Id) s.Send(message);
                    }
                    );
                };

                socket.OnPing = (b) =>
                {
                    socket.SendPong(b);
                };
            });

        }

        public static List<IWebSocketConnection> getSockets()
        {
            return _webSocketsList;
        }

        public static void refreshClientsIssueList(IEnumerable<IssueModel> issueLst)
        {
            _webSocketsList.ForEach(s =>
            {
                s.Send(JsonConvert.SerializeObject(issueLst));
            });
        }
    }
}
