using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Pages
{
    public class CreatePageHandler : IRequestHandler<CreatePage, CreatedEntity<int>>
    {
        private readonly IEntityRepository<Page> _pageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePageHandler(IEntityRepository<Page> pageRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreatePage request, CancellationToken cancellationToken)
        {
            var page = _mapper.Map<CreatePage, Page>(request);
            _pageRepository.Add(page);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _pageRepository.CalculateTree(page);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreatedEntity<int>(page.Id);
        }

    }
}
