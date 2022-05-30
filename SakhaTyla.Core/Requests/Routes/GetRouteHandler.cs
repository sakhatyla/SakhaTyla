using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Routes.Models;

namespace SakhaTyla.Core.Requests.Routes
{
    public class GetRouteHandler : IRequestHandler<GetRoute, RouteModel?>
    {
        private readonly IEntityRepository<Route> _routeRepository;
        private readonly IMapper _mapper;

        public GetRouteHandler(IEntityRepository<Route> routeRepository,
            IMapper mapper)
        {
            _routeRepository = routeRepository;
            _mapper = mapper;
        }

        public async Task<RouteModel?> Handle(GetRoute request, CancellationToken cancellationToken)
        {
            var path = request.Path!.TrimEnd('/');
            var pathWithSlash = path + "/";
            var route = await _routeRepository.GetEntities()
                .Include(e => e.Page)
                .Where(e => e.Path == path || e.Path == pathWithSlash)
                .FirstOrDefaultAsync(cancellationToken);
            if (route == null)
            {
                return null;
            }
            return _mapper.Map<Route, RouteModel>(route);
        }

    }
}
