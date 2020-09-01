using MediatR;
using SakhaTyla.Core.Requests.Profile.Models;

namespace SakhaTyla.Core.Requests.Profile
{
    public class GetProfile : IRequest<ProfileModel>
    {
    }
}
