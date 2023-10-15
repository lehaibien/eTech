using eTech.Context;
using eTech.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services {
    public class UserService {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        public Task<ApplicationUser> GetUserById(string id) {
            return _context.Users.Include(u => u.Image).Include(u => u.Address).Include(u => u.Orders).FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
