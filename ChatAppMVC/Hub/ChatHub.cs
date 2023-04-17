
using ChatAppMVC.Data;
using ChatAppMVC.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppMVC.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task GetNickName(string nickName)
        {

            UserVM model = new UserVM
            {
                ConnectionId = Context.ConnectionId,
                NickName = nickName
            };
            ClientSource.Clients.Add(model);
            if (ClientSource.Clients.Find(x => x.NickName == model.NickName) != null)
            {

                ClientSource.Clients.RemoveAll(x => x.NickName == model.NickName);
            }
            ClientSource.Clients.Add(model);
            await Clients.Others.SendAsync("clientJoined", nickName);
        }

        public async Task GetUser()
        {
            await Clients.All.SendAsync("clients", ClientSource.Clients);
        }

        public async Task SendMessageAsync(string message, string clientName)
        {
            var client = ClientSource.Clients.FirstOrDefault(x => x.NickName == clientName);
            await Clients.Client(client.ConnectionId).SendAsync("receiveMessage", message);
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = ClientSource.Clients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (user is not null)
            {
                await Clients.Others.SendAsync("userLeaved", user.NickName);
                ClientSource.Clients.Remove(user);
            }
        }



    }
}
