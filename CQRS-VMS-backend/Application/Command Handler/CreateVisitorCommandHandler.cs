using Application.Commands;
using Infrastructure.Models;
using Infrastructure.Repository.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Command_Handler
{
    public class CreateVisitorCommandHandler : IRequestHandler<CreateVisitorCommand, Visitor>
    {
        private readonly IVisitorRepository _visitorRepository;

        public CreateVisitorCommandHandler(IVisitorRepository visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        public async Task<Visitor> Handle(CreateVisitorCommand request, CancellationToken cancellationToken)
        {
            var visitorDto = request.VisitorDto;
            var visitor = new Visitor
            {
                Name = visitorDto.Name,
                Phone = visitorDto.PhoneNumber,
                PurposeOfVisit = visitorDto.PurposeOfVisit,
                HostName = visitorDto.PersonInContact,
                OfficeLocation = visitorDto.OfficeLocation,
                StaffId = 1,
                
                VisitorPassCode = 0,
                VisitDate = DateTime.UtcNow,       // Use UTC
                CreatedDate = DateTime.UtcNow,     // Use UTC
                UpdatedDate = DateTime.UtcNow

            };

           

            var createdVisitor = await _visitorRepository.CreateVisitorAsync(visitor);

           

            return createdVisitor;
        }

      
    }

}
