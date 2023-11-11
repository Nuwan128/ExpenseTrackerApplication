using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DevExpress.Utils.IoC;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Data
{
    public class UserServices : IUserServices
    {
        private readonly ExpenseDBContext _dbContext;
        public UserServices(ExpenseDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        public User GetUserById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }
        public User GetUserByUserName(string username)
        {
            return _dbContext.Users.FirstOrDefault(u => u.UserName == username);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }
        public void UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }
        public bool RegisterUser(string username, string plainPassword)
        {
            // Generate a unique salt
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password with the salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword, salt);

            var user = new User
            {
                UserName = username,
                PasswordHash = hashedPassword,
                Salt = salt
            };
            try
            {
                var isHavingUser = _dbContext.Users.FirstOrDefault(u => u.UserName == username);

                if (isHavingUser != null)
                {
                    throw new RegistrationFailedException("Username is already in use.");
                   
                }
                else
                {
                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
                
            }

          
          
        }
        public bool AuthenticateUser(string username, string providedPassword)
        {
            //Retrieve the stored salt and hashed password for the user
            string storedSalt = GetSaltByUsername(username); 
            string storedHashedPassword = GetHashedPasswordByUsername(username); 

            if (storedSalt == null || storedHashedPassword == null)
            {
                // User not found or no password stored
                return false;
            }

            // Hash the provided password with the retrieved salt
            string hashedPasswordToCheck = BCrypt.Net.BCrypt.HashPassword(providedPassword, storedSalt);

            // Compare the hashes
            return hashedPasswordToCheck == storedHashedPassword;
        }
        public string GetSaltByUsername(string username)
        {
            // Query the database to retrieve the salt for the given username
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == username);

            // Return the salt if the user exists, otherwise return null
            return user?.Salt;
        }
        public string GetHashedPasswordByUsername(string username)
        {
            // Query the database to retrieve the hashed password for the given username
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == username);

            // Return the hashed password if the user exists, otherwise return null
            return user?.PasswordHash;
        }

    }


}

