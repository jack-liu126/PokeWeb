namespace PokeWeb.Models
{
    public interface ILoginPost
    {
        string Account { get; set; }
        string Password { get; set; }
    }
}