﻿using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateUserCommand : IRequest<APIResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}
