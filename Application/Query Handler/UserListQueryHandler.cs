using Application.Queries;
using Infrastructure.Models;
using Infrastructure.Models.DTO;
using Infrastructure.Repository;
using Infrastructure.Repository.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query_Handler
{
    public class UserListQueryHandler : IRequestHandler<UserListQuery, IEnumerable<DisplayUserDTO>>
    {
        private readonly IUserRepository _userRepository;
        public UserListQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<DisplayUserDTO>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.UserListAsync();
        }

     
    }
}
