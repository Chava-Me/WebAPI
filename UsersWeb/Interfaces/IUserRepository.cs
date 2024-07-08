using UsersWeb.Models;

namespace UsersWeb.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<bool> Validate(string name, string pass);
    }
}
