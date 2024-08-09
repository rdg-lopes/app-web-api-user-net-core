using UserWebApi.Persistence;

namespace UserWebApi.Service;

public class UserService : IUserService
{
    private IUserPersistence userPersistence;

    public UserService(IUserPersistence userPersistence)
    {
        this.userPersistence = userPersistence;
    }
    public async Task<UserModel> CreateUserAsync(string login, string senha)
    {
        
        int lastIdUser = await this.userPersistence.GetLastIdAsync();
        Console.WriteLine("**************** "+lastIdUser+"*****************");
        Console.WriteLine("**************** "+lastIdUser+"*****************");
        Console.WriteLine("**************** "+lastIdUser+"*****************");
        UserEntity entity = new UserEntity()
        {
            Id = lastIdUser + 1,
            Login = login,
            Senha = senha
        };
        await this.userPersistence.InsertUserAsync(entity);
        UserModel userResult = new UserModel()
        {
            Id = entity.Id,
            Login = entity.Login,
            Senha = entity.Senha
        };
        return userResult;
    }

    public async Task UpdateUserAsync(int id, UserModel user)
    {
        UserEntity userUpdate = new UserEntity()
        {
            Id = user.Id,
            Login = user.Login,
            Senha = user.Senha

        };
        await this.userPersistence.UpdateUserAsync(id, userUpdate);
    }

    public async Task DeleteUserAsync(int id)
    {
        await this.userPersistence.DeleteUserAsync(id);
    }

    public async Task<UserModel?> GetUserByLoginAsync(string login)
    {
        UserEntity? user = await this.userPersistence.GetUserByLoginAsync(login);
        if (user != null){
            UserModel userModel = new UserModel()
            {
                Id = user.Id,
                Login = user.Login,
                Senha = user.Senha
            };
            return userModel;
        }
        return null;
    }

    public async Task<IList<UserModel>> GetAllUsersAsync()
    {
        IList<UserEntity> users = await this.userPersistence.GetAllUsersAsync();
        IList<UserModel> usersResult = new List<UserModel>();
        foreach (UserEntity user in users){
            usersResult.Add(new UserModel()
            {
                Id = user.Id,
                Login = user.Login,
                Senha = user.Senha
            });
        }
        return usersResult;
    }
}