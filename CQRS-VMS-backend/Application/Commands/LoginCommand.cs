using Infrastructure.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class LoginCommand : IRequest<LoginResponseDTO>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
