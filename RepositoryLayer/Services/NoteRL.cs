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
    }
    }

