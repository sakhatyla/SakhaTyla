using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Tags
{
    public class CreateTagHandler : IRequestHandler<CreateTag, CreatedEntity<int>>
    {
        private readonly IEntityRepository<Tag> _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTagHandler(IEntityRepository<Tag> tagRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateTag request, CancellationToken cancellationToken)
        {
            var tag = _mapper.Map<CreateTag, Tag>(request);
            _tagRepository.Add(tag);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreatedEntity<int>(tag.Id);
        }

    }
}
