using System;

namespace QA.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int? QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public int? AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
    }
}