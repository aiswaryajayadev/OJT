using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateUserCommand:IRequest<APIResponse>
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
