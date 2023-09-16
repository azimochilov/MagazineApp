using AutoMapper;
using MagazineApp.Data.IRepositories;
using MagazineApp.Domain.Entities;
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
    private readonly IRepository<User> userRepository;  
    private readonly IMapper mapper;
    public UserService(IRepository<User> userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async ValueTask<UserResultDto> AddAsync(UserCreationDto dto)
    {
        var exist = await this.userRepository.SelectAsync(u => u.Email.Equals(dto.Email));

        if (exist is not null && exist.IsDeleted && PasswordHelper.Verify(dto.Password, exist.Salt, exist.Password))
        {
            var hash = PasswordHelper.Hash(exist.Password);
            exist.IsDeleted = false;
            exist.UpdatedAt = DateTime.UtcNow;
            exist.Password = hash.passwordHash;
            exist.Salt = hash.salt;
            var mappedDto = this.mapper.Map(dto, exist);

            await this.userRepository.SaveAsync();
            return this.mapper.Map<UserResultDto>(exist);
        }
        else if (exist is not null && exist.IsDeleted && !PasswordHelper.Verify(dto.Password, exist.Salt, exist.Password))
        {
            throw new MagazineException(400, "Email or password is wrong");
        }

        else if (exist is not null)
        {
            throw new MagazineException(409, "User already exist");
        }

        var newDto = this.mapper.Map<User>(dto);
        var newHash = PasswordHelper.Hash(dto.Password);
        newDto.Password = newHash.passwordHash;
        newDto.Salt = newHash.salt;
        await this.userRepository.InsertAsync(newDto);
        await this.userRepository.SaveAsync();

        return this.mapper.Map<UserResultDto>(newDto);
    }

    public async ValueTask<UserResultDto> ModifyAsync(UserUpdateDto dto)
    {
        var exist = await this.userRepository.SelectAsync(u => u.Id.Equals(dto.Id));
        if (exist is null)
            throw new MagazineException(404, "Not found");

        if (exist.IsDeleted)
        {
            var updatedDto = this.mapper.Map(dto, exist);
            updatedDto.IsDeleted = false;
            updatedDto.UpdatedAt = DateTime.UtcNow;            
            await this.userRepository.SaveAsync();

            return this.mapper.Map<UserResultDto>(updatedDto);
        }

        var mappedDto = this.mapper.Map(dto, exist);        
        mappedDto.UpdatedAt = DateTime.UtcNow;

        await this.userRepository.SaveAsync();

        return this.mapper.Map<UserResultDto>(mappedDto);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var exist = await this.userRepository.SelectAsync(u => u.Id.Equals(id));
        if (exist is null)
            throw new MagazineException(404, "Not found");

        if (exist.IsDeleted)
            throw new MagazineException(409, "Already deleted");

        await this.userRepository.DeleteAsync(u => u.Id.Equals(id));
        await this.userRepository.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync()
    {
        var users = await userRepository.SelectAll()
            .Where(u => !u.IsDeleted)
            .ToListAsync();

        return mapper.Map<IEnumerable<UserResultDto>>(users);
    }

    public async ValueTask<UserResultDto> RetrieveByIdAsync(long id)
    {
        var user = await this.userRepository.SelectAsync(u => u.Id.Equals(id));
        if (user is null)
            throw new MagazineException(404, "Not found");

        if (user.IsDeleted)
            throw new MagazineException(400, "This accaount is deleted");

        return this.mapper.Map<UserResultDto>(user);
    }
}
