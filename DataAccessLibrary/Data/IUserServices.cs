using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data
{
    public interface IUserServices
    {
        bool AuthenticateUser(string username, string providedPassword);
        void CreateUser(User user);
        void DeleteUser(int id);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByUserName(string username);
        bool RegisterUser(string username, string plainPassword);
        void UpdateUser(User user);
    }
}