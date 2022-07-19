using BusinessLayer.Interface;
using DatabaseLayer.NoteModel;
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
    public class NoteController : Controller
    {
        private readonly ILoggerManager logger;
        private readonly FundooContext fundoContext;
        private readonly INoteBL noteBL;

        public NoteController(FundooContext fundoContext,INoteBL noteBL,ILoggerManager logger)
        {
            this.logger = logger;
            this.fundoContext = fundoContext;
            this.noteBL = noteBL;
        }

        [HttpPost("AddNote")]
        public async Task<IActionResult> AddNote(NotePostModel notePostModel)
        {
            try {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                await this.noteBL.AddNote(UserId, notePostModel);
                this.logger.LogInfo($"Note Created Successfully UserId = {userId}");
                return Ok(new { success = true, Message = "Note Created Successfully...!" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllNote")]
        public async Task<IActionResult>GetAllNote()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                var NoteData = await this.noteBL.GetAllNotes(UserId);
                if (NoteData == null)
                {
                    this.logger.LogInfo($"No notes Exist At Moment!! UserId={UserId}");
                    return this.BadRequest(new { success = false, Message = "You Don't Have Notes...!" });
                }
                this.logger.LogInfo($"All Notes Retrieved Successfully UserId = {userId}");
                return this.Ok(new { sucess = true, Message = "Notes Data Retrieved successfully...", data = NoteData });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
