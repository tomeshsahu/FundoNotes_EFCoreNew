using DatabaseLayer.UserModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        FundooContext fundonotesContext;
        IConfiguration iconfiguration;
        public UserRL(FundooContext fundonotesContext, IConfiguration iconfiguration)
        {
            this.fundonotesContext = fundonotesContext;
            this.iconfiguration = iconfiguration;
        }
        public void AddUser(UserPostModel userPostModel)
        {
            try
            {
                User user = new User();
                user.Firstname = userPostModel.Firstname;
                user.Lastname = userPostModel.Lastname;
                user.Email = userPostModel.Email;
                user.Password = userPostModel.Password;
                user.CreateDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                fundonotesContext.Users.Add(user);
                fundonotesContext.SaveChanges();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
