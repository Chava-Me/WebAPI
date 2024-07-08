
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using UsersWeb.Interfaces;
using UsersWeb.Models;

namespace UsersWeb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _filePath = "users.json";
        private List<User> _users;

        public UserRepository()
        {
            _users = LoadFromFile();
        }

        private List<User> LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }

        private void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(_users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.FromResult(_users);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await Task.FromResult(_users.FirstOrDefault(u => u.UserId == id));
        }

        public async Task AddAsync(User user)
        {
            user.UserId = _users.Any() ? _users.Max(u => u.UserId) + 1 : 1;
            _users.Add(user);
            SaveToFile();
            await Task.CompletedTask;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                _users.Remove(user);
                SaveToFile();
                return true;
            }
            return false;

        }

        public async Task<bool> Validate(string name, string pass)
        {
            var user = _users.FirstOrDefault(u => u.UserName == name && u.Password == pass);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }

}
