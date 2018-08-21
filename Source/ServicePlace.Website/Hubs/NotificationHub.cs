using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using ServicePlace.Website.Models.HubModels;

namespace ServicePlace.Website.Hubs
{
    public class NotificationHub : Hub
    {
        private static readonly ConcurrentDictionary<string, UserHubModel> Users =
            new ConcurrentDictionary<string, UserHubModel>(StringComparer.InvariantCultureIgnoreCase);

        public void SendNotification(string userName, string notificationHtml)
        {
            if (!Users.TryGetValue(userName, out var receiver)) return;
            var cid = receiver.ConnectionIds.FirstOrDefault();
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.Client(cid).broadcastNotif(notificationHtml);
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