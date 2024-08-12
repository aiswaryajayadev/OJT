using Application.Queries;
using Infrastructure.Models;
using Infrastructure.Repository.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query_Handler
{
    public class GetVisitorDetailsQueryHandler : IRequestHandler<GetVisitorDetailsQuery, IEnumerable<Visitor>>
    {
        private readonly IVisitorRepository _visitorRepository;

        public GetVisitorDetailsQueryHandler(IVisitorRepository visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        public async Task<IEnumerable<Visitor>> Handle(GetVisitorDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _visitorRepository.GetVisitorDetailsAsync();
        }
    }

}
