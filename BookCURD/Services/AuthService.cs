namespace BookCURD.Services
{
    using BookCURD.Data;
    using BookCURD.Model;
    using System.Security.Cryptography;
    using System.Text;

    public class AuthService
    {
        private readonly BookContext _context;

        public AuthService(BookContext context)
        {
            _context = context;
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public AuthResponse Authenticate(string username, string password)
        {
            var passwordHash = HashPassword(password);
            var user = _context.Users.SingleOrDefault(u => u.Username == username && u.PasswordHash == passwordHash);

            if (user != null)
            {
                return new AuthResponse
                {
                    Success = true,
                    Role = user.Role,
                    username=user.Username
                
                };
            }

            // Return failure response if authentication fails
            return new AuthResponse
            {
                Success = false,
                Role = null // Role can be null or you can return a specific string like "None"
            };
        }

        public bool IsAdmin(User user)
        {
            return user?.Role == "Admin";
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}
