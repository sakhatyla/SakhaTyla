using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Users.Models;

namespace SakhaTyla.Core.Requests.Users
{
    public class GetUserHandler : IRequestHandler<GetUser, UserModel?>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public GetUserHandler(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<UserModel?> Handle(GetUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(e => e.Roles)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<User, UserModel>(user);
        }
    }
}
