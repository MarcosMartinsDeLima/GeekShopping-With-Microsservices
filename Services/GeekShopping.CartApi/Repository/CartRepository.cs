using AutoMapper;
using GeekShopping.CartApi.Data.ValueObjects;
using GeekShopping.CartApi.Model;
using GeekShopping.Services.CartApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartApi.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MysqlContext _context;
        private IMapper _mapper;

        public CartRepository(MysqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            var Header = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
            if(Header != null)
              {
                Header.CouponCode = couponCode;
                _context.CartHeaders.Update(Header);
                await _context.SaveChangesAsync();
                return true;
              } 

            return false;
        }

        public async Task<bool> ClearCart(string userId)
        {
              var cartHeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
              if(cartHeaderToRemove != null)
              {
                _context.CartDetails.RemoveRange(_context.CartDetails.Where(c => c.CartHeaderId == cartHeaderToRemove.Id));
                _context.CartHeaders.Remove(cartHeaderToRemove);
                await _context.SaveChangesAsync();
                return true;
              }
              return false;
        }

        public async Task<CartVo> FindCartByUserId(string userId)
        {
            Cart cart = new ()
            {
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId) ?? new CartHeader(),
            };

            cart.CartDetails = _context.CartDetails.Where(c => c.CartHeaderId == cart.CartHeader.Id).Include(c => c.Product);
            return _mapper.Map<CartVo>(cart);
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            var Header = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
            if(Header != null)
              {
                Header.CouponCode = "";
                _context.CartHeaders.Update(Header);
                await _context.SaveChangesAsync();
                return true;
              } 
              
            return false;
        }

        public async Task<bool> RemoveFromCart(long carDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _context.CartDetails.FirstOrDefaultAsync(c => c.Id == carDetailsId);
                int total = _context.CartDetails.Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();
                _context.CartDetails.Remove(cartDetail);

                if(total == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public async Task<CartVo> SaveOrUpdateCart(CartVo vo)
        {
            Cart cart = _mapper.Map<Cart>(vo); 
        
            //checa se o produto já esta salvo no banco de dados se não estiver então salva
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == vo.CartDetails.FirstOrDefault().ProductId);

            if(product == null)
            {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            //checa se o cartheader é null
            //AsNoTracking diz pro entity framework para não salvar alterações feitas nessa entidade
            var cartHeadear = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(c => c.UserId ==  cart.CartHeader.UserId);
            if(cartHeadear == null)
            {
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();
                
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }else
            {
                //checa se o cartDetails tem o mesmo produto
                var cartdetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    p =>p.ProductId == cart.CartDetails.FirstOrDefault().ProductId && p.CartHeaderId == cartHeadear.Id );
                
                if(cartdetail == null)
                {
                    //cria cartdetail
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeadear.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();

                }else
                {
                    //atualiza product count e cartdetails
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartdetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartdetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartdetail.CartHeaderId;
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }
            
            return _mapper.Map<CartVo>(cart);
        }
    }
}