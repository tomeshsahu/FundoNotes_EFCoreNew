using DatabaseLayer.NoteModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        Task AddNote(int userId, NotePostModel notePostModel);
        public Task<List<GetNoteModel>> GetAllNotes(int UserId);


    }
}
