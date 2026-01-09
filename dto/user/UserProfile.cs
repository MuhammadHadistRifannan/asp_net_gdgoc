using AutoMapper;
using MyApp.Namespace;

namespace gdgoc_aspnet;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDTO, User>()
        .ForMember(dest => dest.id , opt => opt.Ignore())
        .ForMember(dest => dest.email, opt => opt.Ignore())
        .ForAllMembers(opt => opt.Condition((userdto , user , member) => member != null));


        CreateMap<UserUpdateRequest, User>()
        .ForMember(dest => dest.id , opt => opt.Ignore())
        .ForMember(dest => dest.email, opt => opt.Ignore())
        .ForAllMembers(opt => opt.Condition((userdto , user , member) => member != null));
    }
}
