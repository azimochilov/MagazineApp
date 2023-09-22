using System;

namespace MagazineApp.Service.Helpers;
public class PasswordHelper
{
    public static (string PasswordHash, string Salt) Hash(string password)
    {
        string salt = Guid.NewGuid().ToString();
        string hash = BCrypt.Net.BCrypt.HashPassword(password + salt);
        return (PasswordHash: hash, Salt: salt);
    }

    public static bool Verify(string password, string passwordHash, string salt)
    {
        return BCrypt.Net.BCrypt.Verify(password + salt, passwordHash);
    }
}
