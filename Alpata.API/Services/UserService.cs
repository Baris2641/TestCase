using Alpata.Context;
using Alpata.Models;
using Alpata.Services;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> Register(UserRegisterModel model)
    {
        if (await UserExists(model.Email))
            throw new Exception("User already exists");

        CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            ProfilePicture = model.ProfilePicture != null ? SaveProfilePicture(model.ProfilePicture) : null
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User> Login(LoginModel model)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == model.Email);
        if (user == null || !VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            throw new Exception("Invalid credentials");

        return user;
    }

    public async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    private string SaveProfilePicture(IFormFile profilePicture)
    {
        if (profilePicture == null) throw new ArgumentNullException(nameof(profilePicture));

        // uploads klasörünün mevcut olduğundan emin olun
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        // Dosya ismini benzersiz hale getirmek için bir GUID kullanın
        var uniqueFileName = $"{Guid.NewGuid()}_{profilePicture.FileName}";
        var filePath = Path.Combine(uploadPath, uniqueFileName);

        // Dosyayı kaydet
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            profilePicture.CopyTo(stream);
        }

        return filePath;
    }

    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i]) return false;
            }
        }
        return true;
    }
}
