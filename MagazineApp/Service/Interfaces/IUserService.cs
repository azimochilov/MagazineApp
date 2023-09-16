using MagazineApp.Domain.Entities;
using MagazineApp.Service.DTOs.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagazineApp.Service.Interfaces;
public interface IUserService
{
    ValueTask<UserResultDto> AddAsync(UserCreationDto dto);
    ValueTask<UserResultDto> ModifyAsync(UserUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<UserResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync();
}
