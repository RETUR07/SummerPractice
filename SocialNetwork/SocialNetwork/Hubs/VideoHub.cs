using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Entities.Models;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SocialNetwork.Hubs
{
    public class VideoHub : BaseHub
    {
        public VideoHub(UserManager<User> userManager)
            : base(userManager) { }

        public async Task UploadStream(ChannelReader<string> stream)
        {
            while (await stream.WaitToReadAsync())
            {
                while (stream.TryRead(out var item))
                {
                    string groupname = "video-" + UserId;
                    await Clients.Group(groupname).SendAsync("VideoPart", item);
                }
            }
        }

        public async Task UploadSoundStream(ChannelReader<string> stream)
        {
            while (await stream.WaitToReadAsync())
            {
                while (stream.TryRead(out var item))
                {
                    string groupname = "sound-" + UserId;
                    await Clients.Group(groupname).SendAsync("SoundPart", item);
                }
            }
        }

        public async Task SubscribeStream(string userId)
        {
            string videoGroupname = "video-" + userId;
            string soundGroupname = "sound-" + userId;
            await Groups.AddToGroupAsync(Context.ConnectionId, videoGroupname);
            await Groups.AddToGroupAsync(Context.ConnectionId, soundGroupname);
            await Clients.Caller.SendAsync("Notify", "Subscribed");
        }

        public async Task UnSubscribeStream(string userId)
        {
            string videoGroupname = "video-" + userId;
            string soundGroupname = "sound-" + userId;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, videoGroupname);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, soundGroupname);
            await Clients.Caller.SendAsync("Notify", "UnSubscribed");
        }
    }
}
