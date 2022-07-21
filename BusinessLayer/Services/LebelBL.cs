using BusinessLayer.Interface;
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

        public async Task<bool> Deletelebel(int UserId, int NoteId)
        {
            try
            {
               return await this.lebelRL.DeleteLebel(UserId, NoteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
