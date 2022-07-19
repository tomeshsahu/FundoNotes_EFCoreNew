﻿using BusinessLayer.Interface;
using DatabaseLayer.NoteModel;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public async Task AddNote(int UserId, NotePostModel notePostModel)
        {
            try {
                    await this.noteRL.AddNote(UserId, notePostModel);        
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetNoteModel>> GetAllNotes(int UserId)
        {
            try { 
                return await this.noteRL.GetAllNotes(UserId); 
                }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateNote(int userId, int noteId, UpdateNoteModel updateNoteModel)
        {
            try
            {
                await this.noteRL.UpdateNote(userId, noteId, updateNoteModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
