using Infrastructure.Models.DTO;
using Microsoft.AspNetCore.SignalR;


namespace Infrastructure.VisitorListHub
{
    public class VisitorListHub:Hub
    {
        public async Task SendVisitorList(VisitorCreationDTO visitorCreationDTO)
        {
            await Clients.All.SendAsync("RecieveVisitorList", visitorCreationDTO);
        }
    }
}