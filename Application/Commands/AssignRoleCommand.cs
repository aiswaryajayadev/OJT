using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AssignRoleCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
