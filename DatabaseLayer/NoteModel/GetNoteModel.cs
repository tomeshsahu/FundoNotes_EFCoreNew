using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.NoteModel
{
    public class GetNoteModel
    {

        public int NoteId { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; } 
        public string BgColor { get; set; }
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }    
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        

    }
}
