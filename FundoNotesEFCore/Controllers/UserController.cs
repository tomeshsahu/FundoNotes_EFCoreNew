﻿using BusinessLayer.Interface;
using DatabaseLayer.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLogger.Interface;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FundoNotesEFCore.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class UserController : ControllerBase
        {
        private readonly ILoggerManager logger;
        private FundooContext fundontesContext;
            private IUserBL userBL;
            public UserController(FundooContext fundonotesContext, IUserBL userBL, ILoggerManager logger)
            {
                this.fundontesContext = fundonotesContext;
                this.userBL = userBL;
            this.logger = logger;
            }
            [HttpPost("AddUser")]
            public IActionResult AddUser(UserPostModel userPostModel)
            {
                try
                {
                this.logger.LogInfo($"User Regestration Email : {userPostModel.Email}");
                this.userBL.AddUser(userPostModel);
                    return this.Ok(new { success = true, Message = "User Added SuccessFully..!" });
                }
                catch (Exception ex)
                {
                logger.LogError($"User Regestration Fail: {userPostModel.Email}");
                throw ex;
                }
            }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> getUsers = new List<User>();
                getUsers = this.userBL.GetAllUsers();
                if (getUsers.Count > 0)
                {
                    this.logger.LogInfo($"User Data Retrieved Succesfully...");
                    return Ok(new { success = true, message = "User Data Retrieved Successfully...", data = getUsers });
                }
                this.logger.LogInfo($"No Users Exists at moment in DB...");
                return BadRequest(new { sucess = false, message = "You Dont have any User at the moment in DB!!" });
            }
            catch (Exception ex)
            {
                this.logger.LogInfo($"User Data Retrieve UnSuccesfull...");
                throw ex;
            }
        }

        [HttpPost("LoginUser")]
        public IActionResult LoginUser(UserLoginModel userModel)
        {
            try
            {
                string token = this.userBL.LoginUser(userModel);
                if (token == null)
                {
                    this.logger.LogError($"Login Unsuccessfull : {userModel.Email}");
                    return this.BadRequest(new { success = false, message = "Enter Valid Email and Password!!" });
                }

                this.logger.LogInfo($"User Login Successfull : {userModel.Email}");
                return this.Ok(new { success = true, message = "User Loged In Successfully...", data = token });
            }
            catch (Exception ex)
            {
                this.logger.LogError($"User Login Failed : {userModel.Email}");
                throw ex;
            }
        }

        [HttpPost("ForgetPassword/{email}")]
        public IActionResult ForgetUser(string email)
        {
            try
            {
                bool isExist = this.userBL.ForgetPasswordUser(email);
                if (isExist)
                {
                    this.logger.LogInfo($"Password RestLink Sent Successfully for : {email}");
                    return Ok(new { success = true, message = $"Password Reset Link sent successfully for : {email}" });
                }
                this.logger.LogInfo($"Password RestLink Sent UnSuccessfully for : {email}");
                return BadRequest(new { success = false, message = $"No User Exist with Email : {email}" });
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Something Went Wrong  : {email}");
                throw ex;
            }
        }

        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(PasswordModel modelPassword)
        {
            try
            {
                if (modelPassword.Password != modelPassword.ConfirmPassword)
                {
                    this.logger.LogInfo($"Password Reset UnSuccessfull Due to Invalid Credentials!!!");
                    return this.BadRequest(new { success = false, message = "New Password and Confirm Password are not equal." });
                }

                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var email = claims.Where(p => p.Type == @"Email").FirstOrDefault()?.Value;
                    this.userBL.ResetPassword(email, modelPassword);
                    this.logger.LogInfo($"Password Reset Successfully for : {email}");
                    return this.Ok(new { success = true, message = "Password Reset Sucessfully...", email = $"{email}" });
                }
                else
                {
                    this.logger.LogInfo($"Password Reset UnSuccessfull!!!");
                    return this.BadRequest(new { success = false, message = "Password Reset Unsuccessful!!!" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInfo($"Something went Wrong!!!");
                throw ex;
            }
        }


    }
    }

