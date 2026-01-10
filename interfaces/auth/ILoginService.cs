using MyApp.Namespace;

namespace gdgoc_aspnet;

public interface ILoginService
{   
    public Task<LoginResult> Authenticate(UserLoginRequest _request);
}
