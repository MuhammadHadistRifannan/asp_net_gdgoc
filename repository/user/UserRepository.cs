using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace gdgoc_aspnet;

public class UserRepository : IUserRepository
{
    readonly AppDbContext _dbcontext;
    public UserRepository(AppDbContext _db)
    {
        _dbcontext = _db;
    }

    public async Task<User?> AddUserAsync(User user)
    {
        await _dbcontext.users.AddAsync(user);
        await _dbcontext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        _dbcontext.users.Update(user);
        await _dbcontext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> EmailExist(string email)
    {
        return await _dbcontext.users
        .AsNoTracking()
        .AnyAsync(user => user.email == email);
    }

    public async Task<User?> GetEmailUserAsync(string email)
    {
        var user = await _dbcontext.users
        .AsNoTracking()
        .FirstOrDefaultAsync(user => user.email == email);
        return user;
    }

    public async Task<User?> GetUserByIdAsync(Guid? id)
    {
        var user = await _dbcontext.users
        .AsNoTracking()
        .FirstOrDefaultAsync(user => user.id == id);
        return user;
    }

}
