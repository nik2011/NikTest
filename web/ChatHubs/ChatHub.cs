using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace web
{
    public class ChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void SendMessage(string name, string message)
        {
            //  Clients.All.hello();
            Clients.All.receiveMessage(name, message);
            //用户调用客户端的函数
           
           
        }

        static List<CurrentUser> ConnectedUsers = new List<CurrentUser>();

        public void Connect(string url, int userID)

        {

            var id = Context.ConnectionId;

            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)

            {

                ConnectedUsers.Add(new CurrentUser

                {

                    ConnectionId = id,

                    UserID = userID

                });

                Clients.Caller.onConnected(id, userID, url);

                //Clients.AllExcept(id).onNewUserConnected(id, userID);



                Clients.Client(id).onNewUserConnected(id, userID);

            }

            else

            {

                Clients.Caller.onConnected(id, userID, url);

                Clients.Client(id).onExistUserConnected(id, userID);

                // Clients.AllExcept(id).onExistUserConnected(id, userID);

            }

        }



        /// <summary>

        /// 登出

        /// </summary>

        public void Exit(int userID)

        {

            var id = Context.ConnectionId;

            OnDisconnected();

            Clients.Caller.onConnected(id, userID, "");

            Clients.Client(id).onExit(id, userID);

        }



        /// <summary>

        /// 断开

        /// </summary>

        /// <returns></returns>

        public void  OnDisconnected()

        {

            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

            if (item != null)

            {

                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;

                Clients.All.onUserDisconnected(id, item.UserID);



            }

          

        }



    }


    public class CurrentUser {
        public string ConnectionId;
        public int UserID;
    }
}