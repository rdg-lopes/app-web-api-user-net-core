using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace UserWebApi.Persistence;

[Table("USER_ENTITY")]
public class UserEntity
{
    [Column("ID")]
    public int Id{get;set;}
    [Column("DS_LOGIN")]
    public string Login {get;set;}
    [Column("DS_SENHA")]
    public string Senha {get;set;}
}