using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entity
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string BgColor { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }

        public bool IsPin { get; set; }

        public bool IsArchieve { get; set; }

        public bool IsReminder { get; set; }

        public DateTime RegisteredDate { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
