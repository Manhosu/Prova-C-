using System;

public interface IUserService
{
    bool ValidateCredentials(string email, string password);
    string GenerateToken(string email);
}
