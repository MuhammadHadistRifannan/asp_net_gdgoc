namespace gdgoc_aspnet;

public record LoginResult(string? message , UserDTO? data , string? token);

public record UserRegistRequest(string? email , string? password , string? first_name , string? last_name , string? address);
public record UserLoginRequest(string? email , string? password);
public record UserUpdateRequest(string? password , string? confirm_password , string? first_name , string? last_name , string? address);
