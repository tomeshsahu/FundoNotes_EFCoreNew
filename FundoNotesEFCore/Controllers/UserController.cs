using BusinessLayer.Interface;
using DatabaseLayer.UserModel;
using Microsoft.AspNetCore.Mvc;
using NLogger.Interface;
using RepositoryLayer.Services;
using System;

namespace FundoNotesEFCore.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class UserController : Controller
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
        }
    }

