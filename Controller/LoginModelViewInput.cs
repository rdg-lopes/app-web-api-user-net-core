using System.ComponentModel.DataAnnotations;

namespace UserWebApi.Controllers;

public class LoginModelViewInput
{
    [Required(ErrorMessage = "Id é obrigatório.")]
    public int Id{get;set;}
    [Required(ErrorMessage = "Login é obrigatório.")]
    public string Login{get;set;}
    [Required(ErrorMessage = "Senha é obrigatório.")]
    public string Senha{get;set;}
}

