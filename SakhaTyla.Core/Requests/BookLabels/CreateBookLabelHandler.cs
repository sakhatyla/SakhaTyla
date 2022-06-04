using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class CreateBookLabelHandler : IRequestHandler<CreateBookLabel, CreatedEntity<int>>
    {
        private readonly IEntityRepository<BookLabel> _bookLabelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBookLabelHandler(IEntityRepository<BookLabel> bookLabelRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _bookLabelRepository = bookLabelRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateBookLabel request, CancellationToken cancellationToken)
        {
            var bookLabel = _mapper.Map<CreateBookLabel, BookLabel>(request);
            _bookLabelRepository.Add(bookLabel);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreatedEntity<int>(bookLabel.Id);
        }

    }
}
