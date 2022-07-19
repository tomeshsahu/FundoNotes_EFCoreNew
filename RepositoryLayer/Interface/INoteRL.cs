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

    }
}
