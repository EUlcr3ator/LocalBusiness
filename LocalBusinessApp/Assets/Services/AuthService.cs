using SQLite;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class AuthService
{
    private SQLiteConnection db => SQLiteService.GetConnection();

    public AuthResponse Register(AuthRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            return new AuthResponse { IsSuccess = false, ErrorMessage = "Email or password empty" };

        if (request.Password != request.ConfirmPassword)
            return new AuthResponse { IsSuccess = false, ErrorMessage = "Passwords do not match" };

        if (db.Table<User>().Any(u => u.Email == request.Email))
            return new AuthResponse { IsSuccess = false, ErrorMessage = "Email already registered" };

        string salt = Guid.NewGuid().ToString();
        string hashed = HashPassword(request.Password, salt);

        var user = new User
        {
            Email = request.Email,
            FullName = request.FullName,
            Role = request.Role,
            HashedPassword = hashed,
            Salt = salt
        };

        db.Insert(user);

        return new AuthResponse { IsSuccess = true, UserData = user, Token = Guid.NewGuid().ToString() };
    }

    public AuthResponse Login(AuthRequest request)
    {
        var user = db.Table<User>().FirstOrDefault(u => u.Email == request.Email);
        if (user == null)
            return new AuthResponse { IsSuccess = false, ErrorMessage = "User not found" };

        string hash = HashPassword(request.Password, user.Salt);
        if (user.HashedPassword != hash)
            return new AuthResponse { IsSuccess = false, ErrorMessage = "Invalid password" };

        return new AuthResponse { IsSuccess = true, UserData = user, Token = Guid.NewGuid().ToString() };
    }

    private string HashPassword(string password, string salt)
    {
        return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password + salt)));
    }
}
