using MagazineApp.Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using MagazineApp.Service.DTOs.Stores;

namespace MagazineApp.Service.Interfaces;
public interface IStoreService
{
    Task<StoreResultDto> AddAsync(StoreCreationDto dto);
    Task<StoreResultDto> ModifyAsync(StoreUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<StoreResultDto> RetrieveByIdAsync(long id);        
    Task<IEnumerable<StoreResultDto>> RetrieveAllAsync();    
}
