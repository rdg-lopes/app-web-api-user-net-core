namespace UserWebApi.Persistence;

public interface IUserPersistence
{
    Task InsertUserAsync(UserEntity user);
    Task UpdateUserAsync(int id, UserEntity user);
    Task DeleteUserAsync(int id);
    Task<UserEntity?> GetUserByLoginAsync(string login);
    Task<IList<UserEntity>> GetAllUsersAsync();
    Task<int> GetLastIdAsync();
}