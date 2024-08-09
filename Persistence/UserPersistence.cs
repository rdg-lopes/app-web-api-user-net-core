using Microsoft.EntityFrameworkCore;

namespace UserWebApi.Persistence;

public class UserPersistence : DbContext, IUserPersistence 
{
    private DbSet<UserEntity> users {get;set;}
    public UserPersistence(DbContextOptions options) : base(options)
    {
    }

    public async Task InsertUserAsync(UserEntity user)
    {
        users.Add(user);
        await SaveChangesAsync();
    }

    public async Task UpdateUserAsync(int id, UserEntity user)
    {
        UserEntity? userUpdate = await this.users.Where(u => u.Id == id).FirstAsync();
        if(userUpdate != null)
        {
            userUpdate.Senha = user.Senha;
        }
        await SaveChangesAsync();
    }
    public async Task DeleteUserAsync(int id)
    {
        UserEntity? userUpdate = await this.users.Where(user => user.Id == id).FirstAsync();
        if(userUpdate != null)
        {
            this.users.Remove(userUpdate);
        }
        await SaveChangesAsync();
    }
    public async Task<UserEntity?> GetUserByLoginAsync(string login)
    {
        return await this.users.Where(user => user.Login == login)
                               .FirstAsync();
    }
    public async Task<IList<UserEntity>> GetAllUsersAsync()
    {
        return await this.users.ToListAsync();
    }

    public async Task<int> GetLastIdAsync()
    {
        return await this.users.CountAsync();
    }   
}