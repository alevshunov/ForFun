using System.Linq;

namespace Example2.Repository
{
    public interface IUserRepository
    {
        IQueryable<UserRepository.UserInfo> Users { get; }
    }
}