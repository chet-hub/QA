using System;
using System.Collections.Generic;

namespace QA.Models
{
    public class Question
    {
        
        public int Id { get; set; }
        
        public int Votes { get; set; }
        
        public int Answers { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreateDateTime { get; set; }
        
        public string UserId { get; set; }
        public virtual User User { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
        
        public virtual ICollection<Tag> Tags { get; set; }
        
    }
}