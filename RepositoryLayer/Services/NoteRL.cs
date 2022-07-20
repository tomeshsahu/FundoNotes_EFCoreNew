using DatabaseLayer.NoteModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        private readonly FundooContext fundonoteContext;
        private readonly IConfiguration iconfiguration;

        public NoteRL(FundooContext fundonoteContext, IConfiguration iconfiguration)
        {
            this.fundonoteContext = fundonoteContext;
            this.iconfiguration = iconfiguration;
        }

        public async Task AddNote(int UserId, NotePostModel notePostModel)
        {
            try
            {
                Note note = new Note();
                note.UserId = UserId;
                note.Title = notePostModel.Title;
                note.Description = notePostModel.Description;
                note.BgColor = notePostModel.BgColor;
                note.RegisteredDate = DateTime.Now;
                note.ModifiedDate = DateTime.Now;
                this.fundonoteContext.Notes.Add(note);
                await this.fundonoteContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetNoteModel>> GetAllNotes(int UserId)
        {
            try
            {
                return await fundonoteContext.Users
               .Where(u => u.UserId == UserId)
               .Join(fundonoteContext.Notes,
               u => u.UserId,
               n => n.UserId,
               (u, n) => new GetNoteModel
               {
                   NoteId = n.NoteId,
                   UserId = UserId,
                   Title = n.Title,
                   Description = n.Description,
                   BgColor = n.BgColor,
                   Firstname = u.Firstname,
                   Lastname = u.Lastname,
                   Email = u.Email,
                   CreatedDate = u.CreateDate
               }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateNote(int userId, int noteId, UpdateNoteModel updateNoteModel)
        {
            try
            {
                var UpdateNote = fundonoteContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if (UpdateNote == null||UpdateNote.IsTrash==true)
                {
                    throw new Exception("Note Does Not Exists!!");
                }
                UpdateNote.Title = updateNoteModel.Title;
                UpdateNote.Description = updateNoteModel.Description;
                UpdateNote.BgColor = updateNoteModel.Bgcolor;
                UpdateNote.IsPin = updateNoteModel.IsPin;
                UpdateNote.IsArchieve = updateNoteModel.IsArchive;
                UpdateNote.IsReminder = updateNoteModel.IsRemainder;
                UpdateNote.IsTrash = updateNoteModel.IsTrash;
                UpdateNote.ModifiedDate = DateTime.Now;
                this.fundonoteContext.Notes.UpdateRange(UpdateNote);
                await this.fundonoteContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteNote(int userId, int noteId)
        {
            try
            {
                    var DeleteNote = fundonoteContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                    DeleteNote.IsTrash = true;
                    await this.fundonoteContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task ArchiveNote(int userId, int noteId)
        {
            try
            {
                var note = this.fundonoteContext.Notes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
                if (note != null && note.IsTrash == false)
                {
                    if (note.IsPin == false)
                    {
                        note.IsPin = true;
                    }
                    else
                    {
                        note.IsPin = false;
                    }
                }
                await this.fundonoteContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task PinNote(int UserId, int NoteId)
        {
            try
            {
                var NotePin = fundonoteContext.Notes.FirstOrDefault(x => x.NoteId == NoteId);
                if(NotePin!=null &&  NotePin.IsTrash==false)
                {
                    if(NotePin.IsPin==false)
                    {
                        NotePin.IsPin = true;
                    }
                    else
                    {
                        NotePin.IsPin = false;
                    }
                }
                await this.fundonoteContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task TrashNote(int userId, int noteId)
        {
            try
            {
                var Trash = fundonoteContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if(Trash!=null)
                {
                    if(Trash.IsTrash==false)
                    {
                        Trash.IsTrash = true;
                    }
                    else
                    {
                        Trash.IsTrash = false;
                    }
                }
                await this.fundonoteContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

