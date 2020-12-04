using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Users;

namespace CapstoneBE.Profiles
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //User entity mapper
            CreateMap<CapstoneBEUser, UserInfo>()
                .ForMember(dest =>
                    dest.UserId,
                    opt => opt.MapFrom(src => src.Id));
            CreateMap<UserCreate, CapstoneBEUser>();
        }
    }
}