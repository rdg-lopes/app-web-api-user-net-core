using System.ComponentModel.DataAnnotations;

namespace UserWebApi.Controllers;

public class UserModelViewInput
{
    [Required(ErrorMessage = "O login é obrigatório.")]
    public string Login {get;set;}
    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Senha {get;set;}
}