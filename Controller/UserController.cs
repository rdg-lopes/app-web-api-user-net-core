using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using UserWebApi.Service;

namespace UserWebApi.Controllers;

[ApiController]
[Route("/user")]
[Authorize]
public class UserController : ControllerBase {

    private IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [SwaggerResponse(statusCode:201, description:"Usuário criado com sucesso.", Type = typeof(UserModelViewInput))]
    [SwaggerResponse(statusCode:400, description:"Não foi informado todos os campos obrigatórios o user.", Type = typeof(ErrosModelViewInput))]
    [SwaggerResponse(statusCode:500, description:"Erro interno na api.", Type = typeof(ErrosModelViewInput))]
    [HttpPost]
    [ParametersRequestValidator]
    public async Task<IActionResult> createUser(UserModelViewInput user){
        UserModel userCreated =  await this.userService.CreateUserAsync(user.Login, user.Senha);
        return Created("", userCreated);        
    }

    [SwaggerResponse(statusCode:200, description:"Usuário encontrado com sucesso.", Type = typeof(UserModelView))]
    [SwaggerResponse(statusCode:500, description:"Erro interno na api.", Type = typeof(ErrosModelViewInput))]
    [HttpGet("{login}")]
    public async Task<ActionResult<UserModelView>> GetUserByLogin(string login)
    {
        UserModel? user = await this.userService.GetUserByLoginAsync(login);
        if(user != null){
            UserModelView userResult = new UserModelView()
            {
                Id = user.Id,
                Login = user.Login,
                Senha = user.Senha
            };
            return userResult;
        }
        return NotFound();
    }

    [SwaggerResponse(statusCode:200, description:"Pesquisa realizada com sucesso.", Type = typeof(IList<UserModelView>))]
    [SwaggerResponse(statusCode:500, description:"Erro interno na api.", Type = typeof(ErrosModelViewInput))]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(){
        IList<UserModel> users = await this.userService.GetAllUsersAsync();
        IList<UserModelView> usersResult = new List<UserModelView>();
        foreach(UserModel user in users)
        {
            usersResult.Add(new UserModelView()
            {
                Id = user.Id,
                Login = user.Login,
                Senha = user.Senha
            });
        }
        return Ok(usersResult);
    }
    
    [SwaggerResponse(statusCode:204, description:"Usuário atualizado com sucesso.")]
    [SwaggerResponse(statusCode:400, description:"Não foi informado todos os campos obrigatórios o user.", Type = typeof(ErrosModelViewInput))]
    [SwaggerResponse(statusCode:500, description:"Erro interno na api.", Type = typeof(ErrosModelViewInput))]
    [HttpPut("{id}")]
    [ParametersRequestValidator]
    public async Task<IActionResult> UpdateUser(int id, UserModelViewInput user)
    {
        UserModel userUpdate = new UserModel()
        {
            Id = id,
            Login = user.Login,
            Senha = user.Senha
        };
        await this.userService.UpdateUserAsync(id, userUpdate);
        return NoContent();
    }

    [SwaggerResponse(statusCode:204, description:"Usuário deletad com sucesso.")]
    [SwaggerResponse(statusCode:500, description:"Erro interno na api.", Type = typeof(ErrosModelViewInput))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> deleteUserById(int id)
    {
        await this.userService.DeleteUserAsync(id);
        return NoContent();
    }
}