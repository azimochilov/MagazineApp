using AutoMapper;
using MagazineApp.Data.IRepositories;
using MagazineApp.Domain.Entities;
using MagazineApp.Service.DTOs.Stores;
using MagazineApp.Service.Exceptions;
using MagazineApp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Xps;

namespace MagazineApp.Service.Services;
public class StoreService : IStoreService
{
    private readonly IRepository<Store> storeRepository;
    private readonly IMapper mapper;

    public StoreService(IMapper mapper, IRepository<Store> storeRepository)
    {
        this.mapper = mapper;
        this.storeRepository = storeRepository;
    }

    public async ValueTask<StoreResultDto> AddAsync(StoreCreationDto dto)
    {
        var store = await storeRepository.SelectAsync(srn => srn.Name == dto.Name);
        if (store is not null && !store.IsDeleted)
            throw new MagazineException(409, "Product Already exists");

        var mappedStore = mapper.Map<Store>(dto);
        mappedStore.CreatedAt = DateTime.UtcNow;
        var addedStore = await storeRepository.InsertAsync(mappedStore);

        await storeRepository.SaveAsync();

        return mapper.Map<StoreResultDto>(addedStore);
    }

    public async ValueTask<StoreResultDto> ModifyAsync(StoreUpdateDto dto)
    {
        var store = await storeRepository.SelectAsync(p => p.Id == dto.Id);
        if (store is null || store.IsDeleted)
            throw new MagazineException(404, "Couldn't found store for given Id");

        var modifiedStore = mapper.Map(dto, store);
        modifiedStore.UpdatedAt = DateTime.UtcNow;

        await storeRepository.SaveAsync();

        return mapper.Map<StoreResultDto>(modifiedStore);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var store = await storeRepository.SelectAsync(p => p.Id == id);
        if (store is null || store.IsDeleted)
            throw new MagazineException(404, "Couldn't find store for this given Id");

        await storeRepository.DeleteAsync(p => p.Id == id);
        await storeRepository.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<StoreResultDto>> RetrieveAllAsync()
    {
        var stores = await storeRepository.SelectAll()
            .Where(p => !p.IsDeleted)
            .ToListAsync();

        return mapper.Map<IEnumerable<StoreResultDto>>(stores);
    }

    public async ValueTask<StoreResultDto> RetrieveByIdAsync(long id)
    {
        var store = await storeRepository.SelectAsync(p => p.Id == id);

        if (store is null || store.IsDeleted)
            throw new MagazineException(404, "Store Not Found");

        return mapper.Map<StoreResultDto>(store);
    }
}
