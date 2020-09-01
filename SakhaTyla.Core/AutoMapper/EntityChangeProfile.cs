using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.EntityChanges.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class EntityChangeProfile : Profile
    {
        public EntityChangeProfile()
        {
            CreateMap<EntityChange, EntityChangeModel>();
        }
    }
}
