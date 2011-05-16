using System.Linq;

namespace Example2.Repository
{
    public interface IMailRepository
    {
        IQueryable<MailRepository.MailInfo> Mails { get; }
    }
}