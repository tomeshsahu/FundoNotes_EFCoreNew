﻿using BusinessLayer.Interface;
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
    public class NoteController : ControllerBase
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

        [HttpPut("UpdateNote")]
        public async Task<IActionResult> UpdateNote(int NoteId, UpdateNoteModel updateNoteModel)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var check = this.fundoContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if (check == null || check.IsTrash == true)
                {
                    return this.BadRequest(new { sucess = false, Message = $"No Note Found for NodeId : {NoteId}" });
                }

                if ((updateNoteModel.Title == "") || (updateNoteModel.Title == "string" && updateNoteModel.Description == "string" && updateNoteModel.Bgcolor == "string") )
                {
                    return this.BadRequest(new { sucess = false, Message = "Enter Valid Data" });
                }
               
                    await this.noteBL.UpdateNote(UserId, NoteId, updateNoteModel);
                    return this.Ok(new { sucess = true, Message = "Note Updated Success Fully!!" });
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteNote")]
        public async Task<IActionResult> DeleteNote(int noteId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var updateNote = fundoContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if (updateNote==null || updateNote.IsTrash==true)
                {
                    return this.BadRequest(new { sucess = false, Message = "Note Does not Exists!!" });
                }
                await this.noteBL.DeleteNote(UserId, noteId);
                return Ok(new { success = true, Message = $"NoteId : {noteId} Moved to Trash SuccessFully..." });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("ArchieveNote")]
        public async Task<IActionResult> ArchieveNote(int noteId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var updateNote = fundoContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if (updateNote== null || updateNote.IsTrash==true)
                {
                    this.logger.LogError($"Note Does Not Exists!! {noteId}|UserId = {userId}");
                    return this.BadRequest(new { success = false, Message = "Note Does not Exists!!" });
                }
                await this.noteBL.ArchiveNote(UserId, noteId);
                this.logger.LogInfo($"Note Archived Successfully NoteId={noteId}|UserId = {userId}");
                return this.Ok(new { sucess = true, Message = $"NoteId {noteId} Archived Successfully..." });
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }

        [HttpPut("PinNote")]
        public async Task<IActionResult> PinNote(int noteId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var updateNote = fundoContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if (updateNote == null || updateNote.IsTrash == true)
                {
                    this.logger.LogError($"Note Does Not Exists!! {noteId}|UserId = {userId}");
                    return this.BadRequest(new { success = false, Message = "Note Does not Exists!!" });
                }
                await this.noteBL.PinNote(UserId, noteId);
                this.logger.LogInfo($"Note PinNote Successfully NoteId={noteId}|UserId = {userId}");
                return this.Ok(new { sucess = true, Message = $"NoteId {noteId} PinNote Successfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("TrashNote")]
        public async Task<IActionResult> TrashNote(int noteId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var updateNote = fundoContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if (updateNote == null || updateNote.IsTrash == true)
                {
                    this.logger.LogError($"Note Does Not Exists!! {noteId}|UserId = {userId}");
                    return this.BadRequest(new { success = false, Message = "Note Does not Exists!!" });
                }
                await this.noteBL.PinNote(UserId, noteId);
                this.logger.LogInfo($"Note PinNote Successfully NoteId={noteId}|UserId = {userId}");
                return this.Ok(new { sucess = true, Message = $"NoteId {noteId} Trashed Successfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("ReminderNote/{NoteId}")]

        public async Task<IActionResult> ReminderNote(int NoteId, RemainderNoteModel reminderNoteModel)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var reminder = Convert.ToDateTime(reminderNoteModel.Reminder);
                var result = await this.noteBL.ReminderNote(userId, NoteId, reminder);

                if (result != null)
                {
                    this.logger.LogInfo($"Note Reminder Set Successfully  NoteId={NoteId}|UserId = {userId}");
                    return this.Ok(new { status = 200, success = true, message = result });
                }
                else
                {
                    this.logger.LogError($"Note Reminder Set UnSuccessfull!! {NoteId}|UserId = {userId}");
                    return this.BadRequest(new { success = false, Message = $"NoteId {NoteId} Reminder set Failed!!" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
