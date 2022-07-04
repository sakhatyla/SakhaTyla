using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Core.Requests.Pages
{
    public class GetPageParentsHandler : IRequestHandler<GetPageParents, List<PageModel>>
    {
        private readonly IEntityRepository<Page> _pageRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetPageParentsHandler(IEntityRepository<Page> pageRepository,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<List<PageModel>> Handle(GetPageParents request, CancellationToken cancellationToken)
        {
            var page = _pageRepository.GetEntities()
                .FirstOrDefault(e => e.Id == request.Id);
            if (page == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Страница"], request.Id]);
            }
            var parentIds = page.TreePath!.Split("/")
                .Where(e => !string.IsNullOrEmpty(e))
                .Select(e => int.Parse(e))
                .ToList();
            var pages = await _pageRepository.GetEntities()
                .Include(e => e.Route)
                .Where(e => parentIds.Contains(e.Id))
                .OrderBy(e => e.TreePath)
                .ToListAsync();
            return _mapper.Map<List<Page>, List<PageModel>>(pages);
        }

    }
}
