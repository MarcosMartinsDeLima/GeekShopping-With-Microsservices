using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;
using GeekShopping.Email.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<MysqlContext> _context;

        public EmailRepository(DbContextOptions<MysqlContext>  context)
        {
            _context = context;
        }


        public async Task LogEmail(UpdatePaymentResulMessage message)
        {
            EmailLog email = new EmailLog()
            {
                Email = message.Email,
                SentDate = DateTime.Now,
                Log = $"Order-{message.OrderId} Foi criado com sucesso"
            };
            await using var _db = new MysqlContext(_context);
            _db.Emails.Add(email);
            await _db.SaveChangesAsync();      
        }
    }
}