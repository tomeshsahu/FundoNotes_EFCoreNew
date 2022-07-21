using DatabaseLayer.LebelModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILebelRL
    {
        Task AddLebel(int UserId, int NoteId, string LebelName);

        Task<List<LebelResponseModel>> GetAllLabels(int userId);

        Task<bool> UpdateLebel(int UserId, int NoteId, string LebelName);

        Task<bool> DeleteLebel(int UserId, int NoteId);

        Task<List<LebelResponseModel>>GetLebelByNoteId (int UserId, int NoteId);


    }
}
