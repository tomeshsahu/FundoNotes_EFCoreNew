using DatabaseLayer.NoteModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {

        Task AddNote(int UserId, NotePostModel notePostModel);

        Task <List<GetNoteModel>> GetAllNotes(int UserId);

        Task UpdateNote(int userId, int noteId, UpdateNoteModel updateNoteModel);

        Task DeleteNote(int userId, int noteId);

        Task ArchiveNote(int userId, int noteId);
        Task PinNote(int UserId, int NoteId);


    }
}
