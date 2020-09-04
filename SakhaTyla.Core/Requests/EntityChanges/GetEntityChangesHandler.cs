using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.EntityChanges.Models;

namespace SakhaTyla.Core.Requests.EntityChanges
{
    public class GetEntityChangesHandler : IRequestHandler<GetEntityChanges, PageModel<EntityChangeModel>>
    {
        private readonly IEntityRepository<EntityChange> _entityChangeRepository;
        private readonly IMapper _mapper;

        public GetEntityChangesHandler(IEntityRepository<EntityChange> customerRepository,
            IMapper mapper)
        {
            _entityChangeRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<EntityChangeModel>> Handle(GetEntityChanges request, CancellationToken cancellationToken)
        {
            IQueryable<EntityChange> query = _entityChangeRepository.GetEntities()
                .Include(e => e.CreationUser)
                .Where(e => e.EntityName == request.EntityName);
            if (request.EntityId != null)
            {
                query = query.Where(e => e.EntityId == request.EntityId);
            }
            query = query.OrderByDescending(e => e.CreationDate);
            var entityChanges = await query.ToPagedListAsync(request.PageIndex, request.PageSize);
            var entityChangeModels = entityChanges.Map<EntityChange, EntityChangeModel>(_mapper);
            var diffHelper = new EntityChangeHelper(request.EntityName, _mapper);
            foreach (var entityChangeModel in entityChangeModels.PageItems)
            {
                entityChangeModel.Changes = diffHelper.GetPropertyChanges(entityChangeModel);
            }
            return entityChangeModels;
        }
    }
}
