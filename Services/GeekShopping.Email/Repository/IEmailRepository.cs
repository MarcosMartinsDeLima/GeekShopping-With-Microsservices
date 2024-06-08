using GeekShopping.Email.Messages;

namespace GeekShopping.Email.Repository
{
    public interface IEmailRepository
    {
        Task LogEmail(UpdatePaymentResulMessage message);
    }
}