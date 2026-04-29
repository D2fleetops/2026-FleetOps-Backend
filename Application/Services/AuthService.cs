using Microsoft.EntityFrameworkCore;
using fleetops_backend.Infrastructure.Data;
using fleetops_backend.Models;
using System.Text.RegularExpressions;
namespace fleetops_backend.Application.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthService(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;
            
            return true;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return _tokenService.GenerateToken(user);
        }

        public async Task<string?> RegisterAsync(string fullName, string email, string password)
        {
            if (!IsValidPassword(password))
                throw new InvalidOperationException("Password must be at least 8 characters with uppercase, lowercase, digit, and special character.");

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (existingUser != null)
                return null;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = hashedPassword,
                RoleId = 5,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            return _tokenService.GenerateToken(user!);
        }
    }
}