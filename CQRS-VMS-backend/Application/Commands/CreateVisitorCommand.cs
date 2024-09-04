using Infrastructure.Models;
using Infrastructure.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateVisitorCommand : IRequest<Visitor>
    {

        public VisitorCreationDTO VisitorDto { get; set; }
    }
}
