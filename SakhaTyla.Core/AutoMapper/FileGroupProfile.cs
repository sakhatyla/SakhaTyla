using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.FileGroups;
using SakhaTyla.Core.Requests.FileGroups.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class FileGroupProfile : Profile
    {
        public FileGroupProfile()
        {
            CreateMap<FileGroup, FileGroupModel>();
            CreateMap<FileGroup, FileGroupShortModel>();
            CreateMap<CreateFileGroup, FileGroup>();
            CreateMap<UpdateFileGroup, FileGroup>();
        }
    }
}
