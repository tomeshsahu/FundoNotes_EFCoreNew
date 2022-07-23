using BusinessLayer.Interface;
using DatabaseLayer.LebelModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LebelBL : ILebelBL
    {
        ILebelRL lebelRL;
        public LebelBL(ILebelRL lebelRL)
        {
            this.lebelRL = lebelRL;
        }
        public async Task AddLebel(int UserId, int NoteId, string LebelName)
        {
            try
            {
               await this.lebelRL.AddLebel(UserId, NoteId, LebelName);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Deletelebel(int UserId, int NoteId, int LebelId)
        {
            try
            {
               return await this.lebelRL.DeleteLebel(UserId, NoteId,LebelId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<List<LebelResponseModel>> GetAllLabels(int userId)
        {
            try
            {
                return this.lebelRL.GetAllLabels(userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public  Task<List<LebelResponseModel>> GetLebelByNoteId(int userId, int noteId)
        {
            try
            {
              return this.lebelRL.GetLebelByNoteId(userId, noteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> UpdateLebel(int UserId, int NoteId, int LebelId, string LebelName)
        {
            try
            {
               return await this.lebelRL.UpdateLebel(UserId, NoteId, LebelId, LebelName);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
