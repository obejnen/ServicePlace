using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using ServicePlace.Model.ViewModels.OrderResponseViewModels;
using ServicePlace.Website.Models.HubModels;

namespace ServicePlace.Website.Hubs
{
    public class NotificationHub : Hub
    {
        private static readonly ConcurrentDictionary<string, UserHubModel> Users =
            new ConcurrentDictionary<string, UserHubModel>(StringComparer.InvariantCultureIgnoreCase);

        //Logged Use Call
        public void GetNotification()
        {
            try
            {
                var loggedUser = Context.User.Identity.Name;

                if (!Users.TryGetValue(loggedUser, out var receiver)) return;
                var cid = receiver.ConnectionIds.FirstOrDefault();
                var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                //context.Clients.Client(cid).broadcastNotif(totalNotif);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //Specific User Call
        public void SendNotification(string userName, string notificationHtml)
        {
            try
            {
                if (!Users.TryGetValue(userName, out var receiver)) return;
                var cid = receiver.ConnectionIds.FirstOrDefault();
                var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                context.Clients.Client(cid).broadcastNotif(notificationHtml);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public override Task OnConnected()
        {

            var userName = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, x => new UserHubModel
            {
                UserName = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
                if (user.ConnectionIds.Count == 1)
                {
                    Clients.Others.userConnected(userName);
                }
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userName = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;

            Users.TryGetValue(userName, out var user);

            if (user == null) return base.OnDisconnected(stopCalled);
            lock (user.ConnectionIds)
            {
                user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
                if (user.ConnectionIds.Any()) return base.OnDisconnected(stopCalled);
                Users.TryRemove(userName, out _);
                Clients.Others.userDisconnected(userName);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}