using DatabaseLayer.UserModel;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public void AddUser(UserPostModel userPostModel);
        public List<User> GetAllUsers();
        public string LoginUser(UserLoginModel userLoginModel);
    }
}
