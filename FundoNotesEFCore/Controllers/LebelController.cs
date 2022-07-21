using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLogger.Interface;
using RepositoryLayer.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundoNotesEFCore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LebelController : ControllerBase
    {
        ILebelBL lebelBL;
        FundooContext fundooContext;
        ILoggerManager logger;
        public LebelController(ILebelBL lebelBL, FundooContext fundooContext,ILoggerManager logger)
        {
            this.lebelBL = lebelBL;
            this.fundooContext = fundooContext;
            this.logger = logger;
        }
        [HttpPost("AddLebel")]
        public async Task<IActionResult> AddLebel(int NoteId,string LabelName)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
               await this.lebelBL.AddLebel(UserId,NoteId,LabelName);
                return Ok(new { success = true, message = "Lebel Created Successfully..!" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("Deletelebel")]
        public async Task<IActionResult> Deletelebel(int NoteId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                await this.lebelBL.Deletelebel(UserId, NoteId);
                return Ok(new {succss=true,Message="Lebel Delete Successfully"});
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
