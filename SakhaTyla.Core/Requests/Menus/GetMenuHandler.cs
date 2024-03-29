﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Menus.Models;

namespace SakhaTyla.Core.Requests.Menus
{
    public class GetMenuHandler : IRequestHandler<GetMenu, MenuModel?>
    {
        private readonly IEntityRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public GetMenuHandler(IEntityRepository<Menu> menuRepository,
            IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<MenuModel?> Handle(GetMenu request, CancellationToken cancellationToken)
        {
            var query = _menuRepository.GetEntities();
            if (request.Id != null)
            {
                query = query.Where(e => e.Id == request.Id);
            }
            else if (!string.IsNullOrEmpty(request.Code))
            {
                query = query
                    .Where(e => e.Code == request.Code);
            }
            else
            {
                return null;
            }
            var menu = await query
                .FirstOrDefaultAsync(cancellationToken);
            if (menu == null)
            {
                return null;
            }
            return _mapper.Map<Menu, MenuModel>(menu);
        }

    }
}
