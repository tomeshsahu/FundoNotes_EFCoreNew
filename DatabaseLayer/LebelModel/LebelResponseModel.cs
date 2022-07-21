using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer.LebelModel
{
    public class LebelResponseModel
    {
        [Key]
        public  int LebelId { get; set; }
        public string LebelName { get; set; }
        public int NodeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}
