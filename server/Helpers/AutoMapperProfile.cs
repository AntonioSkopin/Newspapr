using server.Entities;
using server.Models.Auth;
using AutoMapper;

namespace server.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, User>();
        }
    }
}