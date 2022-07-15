using BusinessLayer.Interface;
using DatabaseLayer.UserModel;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;

namespace FundoNotesEFCore.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class UserController : Controller
        {
            private FundooContext fundontesContext;
            private IUserBL userBL;
            public UserController(FundooContext fundonotesContext, IUserBL userBL)
            {
                this.fundontesContext = fundonotesContext;
                this.userBL = userBL;
            }
            [HttpPost("AddUser")]
            public IActionResult AddUser(UserPostModel userPostModel)
            {
                try
                {
                    this.userBL.AddUser(userPostModel);
                    return this.Ok(new { success = true, Message = "User Added SuccessFully..!" });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

