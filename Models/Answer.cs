using System;
using System.Collections.Generic;

namespace QA.Models
{
    public class Answer
    {
        
        public int Id { get; set; }
        
        public int Votes { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreateDateTime { get; set; }
        
        public int QuestionId { get; set; }
        public virtual Question Question{ get; set; }
        
        public string UserId { get; set; }
        public virtual User User { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
    }
}