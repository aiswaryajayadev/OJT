using Application.Commands;
using Infrastructure.Models;
using Infrastructure.Repository.IRepository;
using Infrastructure.VisitorListHub;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Serilog;


namespace Application.Command_Handler
{
    public class CreateVisitorCommandHandler : IRequestHandler<CreateVisitorCommand, Visitor>
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly IHubContext<VisitorListHub> _hubContext;
        private readonly ILogger _logger;

        public CreateVisitorCommandHandler(IVisitorRepository visitorRepository, IHubContext<VisitorListHub> hubContext)
        {
            _visitorRepository = visitorRepository;
            _hubContext = hubContext;
            _logger = Log.ForContext<CreateUserCommandHandler>();
        }

        public async Task<Visitor> Handle(CreateVisitorCommand request, CancellationToken cancellationToken)
        {
            var visitorDto = request.VisitorDto;
           

            _logger.Information("Creating visitor: {@VisitorName}", visitorDto.Name); // Use proper casing for VisitorDto and rename placeholder

            var visitor = new Visitor
            {
                Name = visitorDto.Name,
                Phone = visitorDto.PhoneNumber,
                PurposeOfVisit = visitorDto.PurposeOfVisit,
                HostName = visitorDto.PersonInContact,
                OfficeLocation = visitorDto.OfficeLocation,       
                
                
                VisitDate =  DateTime.UtcNow.Date,      // Use UTC
                CreatedDate = DateTime.UtcNow,     // Use UTC
                UpdatedDate = DateTime.UtcNow

            };

           

            var createdVisitor = await _visitorRepository.CreateVisitorAsync(visitor);

            if (createdVisitor != null)
            {
                await _hubContext.Clients.All.SendAsync("VisitorCreated", createdVisitor);
            }

            return createdVisitor;
        }

      
    }

}
