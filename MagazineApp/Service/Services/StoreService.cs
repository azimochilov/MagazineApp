using AutoMapper;
using MagazineApp.Data.Contexts;
using MagazineApp.Data.IRepositories;
using MagazineApp.Data.Repositories;
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
    private IStoreRepository storeRepository;

    public StoreService()
    {
        var dbContext = new AppDbContext();

        storeRepository = new StoreRepository(dbContext);
    }
    public async Task<StoreResultDto> AddAsync(StoreCreationDto dto)
    {        
        var store = await storeRepository.SelectAsync(srn => srn.Name == dto.Name);
        if (store is not null && !store.IsDeleted)
            throw new MagazineException(409, "Store already exists");
     
        var newStore = new Store
        {
            Name = dto.Name,
            Address = dto.Address,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false 
        };
        
        var addedStore = await storeRepository.InsertAsync(newStore);

        await storeRepository.SaveAsync();

        var resultDto = new StoreResultDto
        {
            Id = addedStore.Id,
            Name = addedStore.Name,
            Address = addedStore.Address
        };

        return resultDto;
    }

    public async Task<StoreResultDto> ModifyAsync(StoreUpdateDto dto)
    {
        var store = await storeRepository.SelectAsync(p => p.Id == dto.Id);
        if (store is null || store.IsDeleted)
            throw new MagazineException(404, "Store not found");
        
        store.Name = dto.Name;
        store.Address = dto.Address;
        store.UpdatedAt = DateTime.UtcNow;
     
        await storeRepository.SaveAsync();

        var resultDto = new StoreResultDto
        {
            Id = store.Id,
            Name = store.Name,
            Address = store.Address
        };

        return resultDto;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var exist = await this.storeRepository.SelectAsync(u => u.Id.Equals(id));
        if (exist is null)
            throw new MagazineException(404, "Not found");

        if (exist.IsDeleted)
            throw new MagazineException(409, "Already deleted");

        await this.storeRepository.DeleteAsync(u => u.Id.Equals(id));
        await this.storeRepository.SaveAsync();

        return true;
    }
    public async Task<IEnumerable<StoreResultDto>> RetrieveAllAsync()
    {
        var stores = await storeRepository.SelectAll()
            .Where(p => !p.IsDeleted)
            .ToListAsync();

        var resultDtos = stores.Select(store => new StoreResultDto
        {
            Id = store.Id,
            Name = store.Name,
            Address = store.Address
        });

        return resultDtos;
    }
    public async Task<StoreResultDto> RetrieveByIdAsync(long id)
    {  
        var store = await storeRepository.SelectAsync(p => p.Id == id);

        if (store is null || store.IsDeleted)
            throw new MagazineException(404, "Store not found");
        
        var resultDto = new StoreResultDto
        {
            Id = store.Id,
            Name = store.Name,
            Address = store.Address
        };

        return resultDto;
    }
}
