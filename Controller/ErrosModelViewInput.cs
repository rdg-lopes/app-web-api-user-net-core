namespace UserWebApi.Controllers;

public class ErrosModelViewInput
{
    public IList<string> Erros {get;set;}

    public ErrosModelViewInput(IList<string> erros)
    {
        this.Erros = erros;
    }

}