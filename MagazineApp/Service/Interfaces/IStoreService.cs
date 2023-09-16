using MagazineApp.Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using MagazineApp.Service.DTOs.Stores;

namespace MagazineApp.Service.Interfaces;
public interface IStoreService
{
    ValueTask<StoreResultDto> AddAsync(StoreCreationDto dto);
    ValueTask<StoreResultDto> ModifyAsync(StoreUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<Store> RetrieveByIdAsync(long id);        
    ValueTask<IEnumerable<StoreResultDto>> RetrieveAllAsync();    
}
