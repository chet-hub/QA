using System;

namespace QA.Models
{
    public class Tag
    {
        public int Id { get; set; }
        
        public Nullable<int> QuestionId { get; set; }
        // public virtual Question Question { get; set; }
        
        public string Name { get; set; }
    }
}