using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Files;
using SakhaTyla.Core.Requests.Files.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<File, FileModel>();
            CreateMap<File, FileShortModel>();
            CreateMap<CreateFile, File>()
                .ForMember(d => d.Content, o => o.Ignore());
            CreateMap<UpdateFile, File>()
                .ForMember(d => d.Content, o => o.Ignore());
        }
    }
}
