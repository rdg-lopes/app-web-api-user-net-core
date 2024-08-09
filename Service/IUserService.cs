namespace UserWebApi.Service;

public interface IUserService
{
    Task<UserModel> CreateUserAsync(string login, string senha);
    Task UpdateUserAsync(int id, UserModel user);
    Task DeleteUserAsync(int id);
    Task<UserModel?> GetUserByLoginAsync(string login);
    Task<IList<UserModel>> GetAllUsersAsync();
}