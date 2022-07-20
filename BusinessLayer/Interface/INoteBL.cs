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

        Task UpdateNote(int userId, int noteId, UpdateNoteModel updateNoteModel);
        Task DeleteNote(int userId, int noteId);
        Task ArchiveNote(int userId, int noteId);
        Task PinNote(int UserId, int NoteId);
        Task TrashNote(int userId, int noteId);


    }
}
