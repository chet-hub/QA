using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QA.Data;
using QA.Models;

interface IValidatable
{
    public List<ValidationResult> Validate();
}

namespace QA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QAController : ControllerBase
    {
        private readonly ILogger<QAController> _logger;
        private readonly ApplicationDbContext context;

        public QAController(ILogger<QAController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public partial class GetQuestionsListParams : IValidatable
        {
            public string orderBy { get; set; }
            public int from { get; set; }
            public int to { get; set; }
            public int? tag { get; set; }

            public List<ValidationResult> Validate()
            {
                var results = new List<ValidationResult>();
                if (!(orderBy.Equals("CreateDateTime") || orderBy.Equals("Answers")))
                {
                    results.Add(new ValidationResult("orderBy must be time or answers count",
                        new string[] {"orderBy"}));
                }

                if (from < 0 || to <= 0)
                {
                    results.Add(new ValidationResult("from and to must great than 0", new string[] {"orderBy"}));
                }

                if (from >= to)
                {
                    results.Add(new ValidationResult("from must less than to", new string[] {"orderBy"}));
                }

                return results;
            }
        }


        [Route("/GetQuestionsList")]
        [HttpPost]
        public ActionResult GetQuestionsList([FromBody] GetQuestionsListParams paramObject)
        {
            var re = paramObject.Validate();
            if (re.Any())
            {
                return NotFound(new JsonResult(re));
            }
            else
            {
                var tags = context.Tags.SingleOrDefault(tag => tag.Id == paramObject.tag);
                    
                var result = context.Questions
                    .Include(q => q.User)
                    .Include(q => q.Tags)
                    .Include(q => q.Comments)
                    .Where(p => tags == null || p.Tags.Any(tag => tag.Name.Equals(tags.Name)));
                var total = result.Count();    
                var questions = result.OrderByDescending(p =>
                        paramObject.orderBy.Equals("CreateDateTime") ? p.CreateDateTime.Millisecond : p.Answers)
                    .Skip(paramObject.from)
                    .Take(paramObject.to).ToArray();
                return new JsonResult(new Hashtable
                {
                    {"questions", questions},
                    {"total", total}
                });
            }
        }

        public partial class PostQuestionParam : IValidatable
        {
            public string userId { get; set; }
            public string title { get; set; }
            public string description { get; set; }

            public string tags { get; set; }

            public List<ValidationResult> Validate()
            {
                var results = new List<ValidationResult>();
                if (string.IsNullOrEmpty(userId))
                {
                    results.Add(new ValidationResult("userId is null or empty",
                        new List<string> {"userId"}));
                }

                if (string.IsNullOrEmpty(title))
                {
                    results.Add(new ValidationResult("title is null or empty",
                        new List<string> {"title"}));
                }

                if (string.IsNullOrEmpty(description))
                {
                    results.Add(new ValidationResult("description is null or empty",
                        new List<string> {"description"}));
                }

                if (string.IsNullOrEmpty(tags) || tags.Split(",").Any(t => string.IsNullOrEmpty(t.Trim())))
                {
                    results.Add(new ValidationResult("no tags or any tag is empty string",
                        new List<string> {"tags"}));
                }

                return results;
            }
        }

        [Authorize]
        [Route("/PostQuestion")]
        [HttpPost]
        public ActionResult PostQuestion([FromBody] PostQuestionParam param)
        {
            var re = param.Validate();
            if (re.Any())
            {
                return NotFound(new JsonResult(re));
            }
            else
            {
                var question = new Question();
                question.Answers = 0;
                // question.Comments = new List<Comment>();
                question.Description = param.description;
                // question.Tags = new List<Tag>();
                question.Title = param.title;
                question.Votes = 0;
                question.UserId = param.userId;
                question.CreateDateTime = DateTime.Now;
                context.Questions.Add(question);
                if (context.SaveChanges() == 1)
                {
                    var tags = new List<Tag>();

                    new List<string>(param.tags.Split(",")).ForEach(tagstr =>
                    {
                        tags.Add(new Tag
                        {
                            Name = tagstr.Trim(),
                            QuestionId = question.Id
                        });
                    });
                    context.Tags.AddRange(tags);
                    if (context.SaveChanges() == tags.Count)
                    {
                        return new JsonResult(question.Id);
                    }
                }

                return StatusCode(500);
            }
        }


        public partial class VoteQuestionParam : IValidatable
        {
            public int questionId { get; set; }
            public int vote { get; set; }

            public List<ValidationResult> Validate()
            {
                var results = new List<ValidationResult>();
                if (questionId == 0)
                {
                    results.Add(new ValidationResult("questionId is null",
                        new List<string> {"questionId"}));
                }

                if (!(vote == 1 || vote == -1))
                {
                    results.Add(new ValidationResult("vote is -1 or 1",
                        new List<string> {"vote"}));
                }

                return results;
            }
        }

        [Authorize]
        [Route("/VoteQuestion")]
        [HttpPost]
        public ActionResult VoteQuestion([FromBody] VoteQuestionParam param)
        {
            var re = param.Validate();
            if (re.Any())
            {
                return NotFound();
            }
            else
            {
                var result = context.Questions.Where(q => q.Id == param.questionId).ToArray();
                if (result.Length == 1)
                {
                    result[0].Votes += param.vote;
                    if (context.SaveChanges() == 1)
                    {
                        return new JsonResult("ok");
                    }
                }

                return StatusCode(500);
            }
        }

        public partial class VoteParam : IValidatable
        {
            public string userId { get; set; }
            public int vote { get; set; }

            public string type { get; set; }

            public string id { get; set; }

            public List<ValidationResult> Validate()
            {
                var results = new List<ValidationResult>();
                if (string.IsNullOrEmpty(userId))
                {
                    results.Add(new ValidationResult("userId is null",
                        new List<string> {"userId"}));
                }

                if (string.IsNullOrEmpty(type))
                {
                    results.Add(new ValidationResult("type is null",
                        new List<string> {"type"}));
                }

                if (string.IsNullOrEmpty(id))
                {
                    results.Add(new ValidationResult("id for the object to vote must not be null",
                        new List<string> {"type"}));
                }

                if (!(vote == 1 || vote == -1))
                {
                    results.Add(new ValidationResult("vote is -1 or 1",
                        new List<string> {"vote"}));
                }

                return results;
            }
        }

        [Authorize]
        [Route("/Vote")]
        [HttpPost]
        public ActionResult Vote([FromBody] VoteParam param)
        {
            var re = param.Validate();
            if (re.Any())
            {
                return NotFound();
            }
            else
            {
                string userId = "";
                int votes = 0;
                //Vote.Type = {question:"question",answer:"answer",user:"user"}
                if (param.type.Equals("user"))
                {
                    return NotFound();
                }
                else if ((param.type.Equals("question")))
                {
                    var qs = context.Questions
                        .Where(q => q.Id == int.Parse(param.id))
                        .ToArray();
                    if (qs.Length == 1)
                    {
                        if (qs[0].UserId.Equals(param.userId))
                        {
                            return StatusCode(403, "shouldn't vote your own question or answer");
                        }

                        qs[0].Votes += param.vote;
                        userId = qs[0].UserId;
                        context.SaveChanges();
                        votes = qs[0].Votes;
                    }
                }
                else if ((param.type.Equals("answer")))
                {
                    var answer = context.Answers
                        .Where(a => a.Id == int.Parse(param.id))
                        .ToArray();
                    if (answer.Length == 1)
                    {
                        if (answer[0].UserId.Equals(param.userId))
                        {
                            return StatusCode(406, "shouldn't vote your own question or answer");
                        }

                        answer[0].Votes += param.vote;
                        userId = answer[0].UserId;
                        context.SaveChanges();
                        votes = answer[0].Votes;
                    }
                }
                else
                {
                    return NotFound();
                }

                var result = context.Users.Where(q => q.Id.Equals(userId)).ToArray();
                if (result.Length == 1)
                {
                    result[0].Reputation += param.vote;
                    if (context.SaveChanges() == 1)
                    {
                        return new JsonResult(votes);
                    }
                }

                return StatusCode(500);
            }
        }


        public partial class GetAnswersListParam : IValidatable
        {
            public int questionId { get; set; }

            public List<ValidationResult> Validate()
            {
                var results = new List<ValidationResult>();
                if (questionId == 0)
                {
                    results.Add(new ValidationResult("questionId is 0",
                        new List<string> {"questionId"}));
                }

                return results;
            }
        }

        [Route("/GetAnswersList")]
        [HttpPost]
        public ActionResult GetAnswersList([FromBody] GetAnswersListParam param)
        {
            var re = param.Validate();
            if (re.Any())
            {
                return NotFound();
            }
            else
            {
                var question = context.Questions
                    .Where(q => q.Id == param.questionId)
                    .Include(q => q.User)
                    .Include(q => q.Tags)
                    .Include(q => q.Comments)
                    .ToArray();
                var answers = context.Answers
                    .Where(answer => answer.QuestionId == param.questionId)
                    .Include(q => q.User)
                    .Include(q => q.Comments)
                    .ToArray();
                if (question.Length == 1)
                {
                    return new JsonResult(new Hashtable
                    {
                        {"question", question[0]},
                        {"answers", answers}
                    });
                }
                else
                {
                    return NotFound();
                }
            }
        }

        public partial class PostAnswerParam : IValidatable
        {
            public int questionId { get; set; }
            public string userId { get; set; }
            public string description { get; set; }

            public List<ValidationResult> Validate()
            {
                var results = new List<ValidationResult>();
                if (questionId == 0)
                {
                    results.Add(new ValidationResult("questionId is 0",
                        new List<string> {"questionId"}));
                }

                if (string.IsNullOrEmpty(userId))
                {
                    results.Add(new ValidationResult("userId is empty",
                        new List<string> {"userId"}));
                }

                if (string.IsNullOrEmpty(description))
                {
                    results.Add(new ValidationResult("description is empty",
                        new List<string> {"description"}));
                }

                return results;
            }
        }

        [Authorize]
        [Route("/PostAnswer")]
        [HttpPost]
        public ActionResult PostAnswer([FromBody] PostAnswerParam param)
        {
            var re = param.Validate();
            if (re.Any())
            {
                return NotFound();
            }
            else
            {
                Answer answer = new Answer();
                answer.QuestionId = param.questionId;
                answer.UserId = param.userId;
                answer.Description = param.description;
                answer.CreateDateTime = DateTime.Now;
                context.Answers.Add(answer);

                if (context.SaveChanges() == 1)
                {
                    var question = context.Questions
                        .Where(q => q.Id == answer.QuestionId)
                        .Include(q => q.User)
                        .Include(q => q.Comments)
                        .Include(q => q.Tags)
                        .ToArray();
                    if (question.Length == 1)
                    {
                        question[0].Answers += 1;
                        context.Update(question[0]);
                        context.SaveChanges();
                    }
                    else
                    {
                        return StatusCode(500);
                    }

                    var answers1 = context.Answers
                        .Where(answer => answer.QuestionId == param.questionId)
                        .Include(q => q.User)
                        .Include(q => q.Comments)
                        .ToArray();
                    return new JsonResult(new Hashtable
                    {
                        {"question", question[0]},
                        {"answers", answers1}
                    });
                }
                else
                {
                    return StatusCode(500);
                }
            }
        }


        public partial class PostCommentParam : IValidatable
        {
            public int questionId { get; set; }
            public int answerId { get; set; }
            public string userId { get; set; }
            public string description { get; set; }

            public List<ValidationResult> Validate()
            {
                var results = new List<ValidationResult>();
                if ((questionId == 0 && answerId == 0) || (questionId != 0 && answerId != 0))
                {
                    results.Add(new ValidationResult("have to provide one of value in questionId and answerId",
                        new List<string> {"questionId", "answerId"}));
                }

                if (string.IsNullOrEmpty(userId))
                {
                    results.Add(new ValidationResult("userId is empty",
                        new List<string> {"userId"}));
                }

                if (string.IsNullOrEmpty(description))
                {
                    results.Add(new ValidationResult("description is empty",
                        new List<string> {"description"}));
                }

                return results;
            }
        }

        [Authorize]
        [Route("/PostComment")]
        [HttpPost]
        public ActionResult PostComment([FromBody] PostCommentParam param)
        {
            var re = param.Validate();
            if (re.Any())
            {
                return NotFound();
            }
            else
            {
                var comment = new Comment();
                comment.Description = param.description;
                comment.CreateDateTime = DateTime.Now;
                comment.UserId = param.userId;
                comment.AnswerId = param.answerId == 0 ? null : param.answerId;
                comment.QuestionId = param.questionId == 0 ? null : param.questionId;
                context.Comments.Add(comment);
                if (context.SaveChanges() == 1)
                {
                    var users = context.Users
                        .Where(user => user.Id.Equals(comment.UserId))
                        .ToArray();
                    if (users.Length != 1)
                    {
                        return StatusCode(500);
                    }

                    comment.User = users[0];
                    return new JsonResult(comment);
                }
                else
                {
                    return StatusCode(500);
                }
            }
        }
    }
}