using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Serilog;
using Simple_Authentication_System_Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Authentication_System_Infrastructure.Repositories
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly ApplicationDbContext _context;
        private IUserRepository _userRepository;
        private readonly ILogger<UnitOfWork> _logger;


        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
