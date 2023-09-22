using MagazineApp.Domain.Entities;
using MagazineApp.Service.DTOs.Stores;
using MagazineApp.Service.DTOs.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagazineApp.Service.Interfaces;
public interface IUserService
{
    Task<User> AddAsync(UserCreationDto dto);
    Task<IEnumerable<UserResultDto>> RetrieveAllAsync();
    Task<UserResultDto> RetrieveByIdAsync(long id);

}
