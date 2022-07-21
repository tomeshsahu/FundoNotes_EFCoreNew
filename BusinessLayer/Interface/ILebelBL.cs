using DatabaseLayer.LebelModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILebelBL
    {
        Task AddLebel(int UserId, int NoteId, string LebelName);

        Task<bool>Deletelebel(int UserId, int NoteId, int LebelId);
        Task<List<LebelResponseModel>> GetAllLabels(int userId);
        Task<bool> UpdateLebel(int UserId, int NoteId,int LebelId, string LebelName);
    }
}
