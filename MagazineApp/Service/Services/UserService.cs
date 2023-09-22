using AutoMapper;
using MagazineApp.Data.Contexts;
using MagazineApp.Data.IRepositories;
using MagazineApp.Data.Repositories;
using MagazineApp.Domain.Entities;
using MagazineApp.Service.DTOs.Stores;
using MagazineApp.Service.DTOs.Users;
using MagazineApp.Service.Exceptions;
using MagazineApp.Service.Helpers;
using MagazineApp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineApp.Service.Services;
public class UserService : IUserService
{
    private IUserRepository userRepository;

    public UserService()
    {
        var dbContext = new AppDbContext();

        userRepository = new UserRepository(dbContext);
    }

    public async Task<User> AddAsync(UserCreationDto dto)
    {
        var exist = await this.userRepository.SelectAsync(u => u.FirstName == dto.FirstName);

        if (exist is not null && exist.IsDeleted && PasswordHelper.Verify(dto.Password, exist.Salt, exist.Password))
        {
            var hash = PasswordHelper.Hash(exist.Password);
            exist.IsDeleted = false;
            exist.UpdatedAt = DateTime.UtcNow;
            exist.Password = hash.PasswordHash;
            exist.Salt = hash.Salt;

            await this.userRepository.SaveAsync();
            return exist;
        }
        else if (exist is not null && exist.IsDeleted && !PasswordHelper.Verify(dto.Password, exist.Salt, exist.Password))
        {
            throw new MagazineException(400, "Email or password is wrong");
        }

        else if (exist is not null)
        {
            throw new MagazineException(409, "User already exist");
        }

        var newDto = new User
        {
            Phone = dto.PhoneNumber,
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Email = dto.Email,
            Password = dto.Password,
        };
        var newHash = PasswordHelper.Hash(dto.Password);
        newDto.Password = newHash.PasswordHash;
        newDto.Salt = newHash.Salt;
        await this.userRepository.InsertAsync(newDto);
        await this.userRepository.SaveAsync();

        return newDto;
    }


    public async Task<IEnumerable<UserResultDto>> RetrieveAllAsync()
    {
        var users = await userRepository.SelectAll()
            .Where(p => !p.IsDeleted)
            .ToListAsync();

        // Create result DTOs from the list of stores
        var resultDtos = users.Select(store => new UserResultDto
        {
            Id = store.Id,
            FirstName = store.FirstName,
            LastName = store.LastName,
            Email = store.Email,
            PhoneNumber = store.Phone            
        });

        return resultDtos;
    }

    public async Task<UserResultDto> RetrieveByIdAsync(long id)
    {
        // Find the store by ID
        var user = await userRepository.SelectAsync(p => p.Id == id);

        if (user is null || user.IsDeleted)
            throw new MagazineException(404, "User not found");

        // Create a result DTO from the store
        var resultDto = new UserResultDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.Phone
        };

        return resultDto;
    }

}
