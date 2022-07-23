using BusinessLayer.Interface;
using DatabaseLayer.LebelModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLogger.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> Deletelebel(int NoteId,int LebelId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                await this.lebelBL.Deletelebel(UserId, NoteId, LebelId);
                return Ok(new {succss=true,Message="Lebel Delete Successfully"});
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAlllebel")]
        public async Task<IActionResult> GetAllLabels()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                var result = await this.lebelBL.GetAllLabels(UserId);
                return this.Ok(new { success = true, Message = "Fetch all labels", data = result });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("UpdateLebel")]
        public async Task<IActionResult> UpdateLebel(int NoteId,int LebelId, string LebelName)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                this.lebelBL.UpdateLebel(UserId, NoteId, LebelId, LebelName);
                return this.Ok(new { success = true, Message = " Lebel updated Succssfully...!" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetLabelByNoteid")]
        public async Task<List<LebelResponseModel>> GetLebelByNoteId(int NoteId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                var NoteData = await this.lebelBL.GetLebelByNoteId(UserId, NoteId);
                return NoteData;
              
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
