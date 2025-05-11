using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Simple_Authentication_System_Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Authentication_System_Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> UserExistsAsync(string email, string username)
        {
            return await _context.Users.AnyAsync(u =>
                u.Email.ToLower() == email.ToLower() ||
                u.Username.ToLower() == username.ToLower());
        }
    }
}
