using eTech.Context;
using eTech.Entities;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eTech.Services {
    public class CartService: ICartService {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
        public CartService(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        public Task<List<CartItem>> GetAll() {
            throw new NotImplementedException();
        }

        public Task<CartItem> GetById(int Id) {
            throw new NotImplementedException();
        }

        public Task<List<CartItem>> GetByUserId(string userId) {
            return _context.CartItems.Where(c => c.UserId == userId).Include(c => c.Product).Include(c => c.Product.Brand).Include(c => c.Product.Images).ToListAsync();
        }

        public async Task<CartItem> Add(CartItem cart) {
            if(cart == null) {
                throw new ArgumentNullException(nameof(cart));
            }
            cart.User = _userManager.FindByIdAsync(cart.UserId).Result;
            cart.Product = await _context.Products.Include(p => p.Images).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == cart.ProductId);
            CartItem c = await _context.CartItems.FirstOrDefaultAsync(c => c.ProductId == cart.ProductId && c.UserId == cart.UserId);
            if(c != null) {
                c.Quantity += cart.Quantity;
                _context.CartItems.Update(c);
                await _context.SaveChangesAsync();
                return c;
            }
            _context.CartItems.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public Task<CartItem> Update(CartItem cart) {
            throw new NotImplementedException();
        }

        public Task Delete(int id) {
            throw new NotImplementedException();
        }
    }
}
