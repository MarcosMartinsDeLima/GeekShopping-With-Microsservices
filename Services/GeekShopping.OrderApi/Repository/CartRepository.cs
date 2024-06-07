using GeekShopping.OrderApi.Model;
using GeekShopping.OrderApi.Model.Context;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<MysqlContext> _context;

        public OrderRepository(DbContextOptions<MysqlContext>  context)
        {
            _context = context;
        }

        public async Task<bool> AddOrder(OrderHeader header)
        {
            if(header == null) return false;
            await using var _db = new MysqlContext(_context);
            _db.Headers.Add(header);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool status)
        {

            await using var _db = new MysqlContext(_context);
            var header = await _db.Headers.FirstOrDefaultAsync(o => o.Id == orderHeaderId);
            if(header != null) 
            {
                header.PaymentStatus = status;
                await _db.SaveChangesAsync();
            }     
        }
    }
}