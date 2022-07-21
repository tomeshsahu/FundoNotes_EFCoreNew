using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILebelBL
    {
        Task AddLebel(int UserId, int NoteId, string LebelName);

        Task<bool>Deletelebel(int UserId, int NoteId);
    }
}
