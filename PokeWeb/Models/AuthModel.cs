namespace PokeWeb.Models;


public class LoginPost : ILoginPost
{
    public string Account { get; set; }
    public string Password { get; set; }
}