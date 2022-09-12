using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using System.Threading.Tasks;

namespace SocialNetwork.Hubs
{
    [Authorize]
    public class RateHub : Hub
    {
    }
}
