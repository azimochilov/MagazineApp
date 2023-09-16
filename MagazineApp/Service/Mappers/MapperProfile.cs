using AutoMapper;
using MagazineApp.Domain.Entities;
using MagazineApp.Service.DTOs.Stores;
using MagazineApp.Service.DTOs.Users;

namespace MagazineApp.Service.Mappers;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<UserCreationDto, UserResultDto>().ReverseMap();

        CreateMap<Store, StoreCreationDto>().ReverseMap();
        CreateMap<StoreCreationDto, StoreResultDto>().ReverseMap();
        CreateMap<Store, StoreResultDto>().ReverseMap();
        CreateMap<Store, StoreUpdateDto>().ReverseMap();
    }
}
