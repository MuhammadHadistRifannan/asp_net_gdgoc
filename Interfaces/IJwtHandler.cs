namespace gdgoc_aspnet;

public interface IJwtHandler
{
    public string Generate(User user);
}
