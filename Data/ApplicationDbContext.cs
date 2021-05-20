using QA.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace QA.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<User>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<Answer> Answers { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Comment> Comments { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Question> Questions { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Tag> Tags { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string userId0 = "d2eac9d3-6141-476c-9020-f42250d63e86";
            string userId1 = "d985a7b1-d58b-4266-ab86-a0a0ff91ccc1";
            string userId2 = "f984066c-816d-4531-bd9a-c63256ca7000";

            // cC(182066202)
            string password = "AQAAAAEAACcQAAAAEGRqPFKkbruQdNbgz4NIUFNiO7cchHhnpFbx7mTM2esETqm1b07MguuCUDdLD7K4hQ==";

            // user and role
            builder.Entity<User>().HasData(new List<User>
            {
                new User
                {
                    Id = userId0,
                    Reputation = 0,
                    UserName = "ff@qq.com",
                    NormalizedUserName = "FF@QQ.COM",
                    Email = "ff@qq.com",
                    NormalizedEmail = "FF@QQ.COM",
                    EmailConfirmed = true,
                    PasswordHash = password,
                    SecurityStamp = "N4Q2NOPGJ5DYCTUU67NCDN6EELEJFO4N",
                    ConcurrencyStamp = "3b3e029c-9d49-4d25-968b-9c3a913d903c",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new User
                {
                    Id = userId1,
                    Reputation = 0,
                    UserName = "cc861010@gmail.com",
                    NormalizedUserName = "CC861010@GMAIL.COM",
                    Email = "cc861010@gmail.com",
                    NormalizedEmail = "CC861010@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = password,
                    SecurityStamp = "USIVDWV4UHHSCT6ZTTJDMSRXHVVU4P2D",
                    ConcurrencyStamp = "714d2b03-873f-479b-9249-4e7ef30866f9",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new User
                {
                    Id = userId2,
                    Reputation = 0,
                    UserName = "cc@qq.com",
                    NormalizedUserName = "CC@QQ.COM",
                    Email = "cc@qq.com",
                    NormalizedEmail = "CC@QQ.COM",
                    EmailConfirmed = true,
                    PasswordHash = password,
                    SecurityStamp = "BNDKY6ICQC3YUUQL5OTFI2277AJY7LGJ",
                    ConcurrencyStamp = "dd5e1fdf-03ea-47f2-84c7-2873d0ae85b9",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole[]
            {
                new IdentityRole
                {
                    Id = "d2eac9d3-6141-476c-9020-f42250d63e80",
                    Name = "Admin",
                },
                new IdentityRole
                {
                    Id = "d2eac9d3-6141-476c-9020-f42250d63e81",
                    Name = "Guest"
                },
                new IdentityRole
                {
                    Id = "d2eac9d3-6141-476c-9020-f42250d63e82",
                    Name = "Moderator"
                }
            });

            builder.Entity<IdentityRoleClaim<string>>().HasData(new IdentityRoleClaim<string>[]
            {
                new IdentityRoleClaim<string>
                {
                    Id = -1,
                    RoleId = "d2eac9d3-6141-476c-9020-f42250d63e80",
                    ClaimType = "abc",
                    ClaimValue = "1"
                },

                new IdentityRoleClaim<string>
                {
                    Id = -2,
                    RoleId = "d2eac9d3-6141-476c-9020-f42250d63e81",
                    ClaimType = "b",
                    ClaimValue = "1"
                },

                new IdentityRoleClaim<string>
                {
                    Id = -3,
                    RoleId = "d2eac9d3-6141-476c-9020-f42250d63e82",
                    ClaimType = "c",
                    ClaimValue = "1"
                },
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = "d2eac9d3-6141-476c-9020-f42250d63e80",
                    UserId = "d2eac9d3-6141-476c-9020-f42250d63e86"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "d2eac9d3-6141-476c-9020-f42250d63e81",
                    UserId = "d985a7b1-d58b-4266-ab86-a0a0ff91ccc1"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "d2eac9d3-6141-476c-9020-f42250d63e82",
                    UserId = "f984066c-816d-4531-bd9a-c63256ca7000"
                },
            });

            //question

            builder.Entity<Tag>().HasData(new Tag[]
            {
                new Tag
                {
                    Id = -1,
                    Name = "iphone",
                    QuestionId = -1
                },
                new Tag
                {
                    Id = -2,
                    Name = "apple",
                    QuestionId = -2
                },
                new Tag
                {
                    Id = -3,
                    Name = "android",
                    QuestionId = -3
                },
                new Tag
                {
                    Id = -4,
                    Name = "c#",
                    QuestionId = -4
                },
                new Tag
                {
                    Id = -5,
                    Name = "java",
                    QuestionId = -5
                },
                new Tag
                {
                    Id = -6,
                    Name = "js",
                    QuestionId = -6
                }
            });


            builder.Entity<Question>().HasData(new Question[]
            {
                new Question
                {
                    Id = -1,
                    Votes = 12,
                    Answers = 1,
                    Title = "Question title -1 ",
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Question
                {
                    Id = -2,
                    Votes = 12,
                    Answers = 1,
                    Title = "Question title -2",
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Question
                {
                    Id = -3,
                    Votes = 12,
                    Answers = 1,
                    Title = "Question title -3",
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Question
                {
                    Id = -4,
                    Votes = 12,
                    Answers = 1,
                    Title = "Question title -4",
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Question
                {
                    Id = -5,
                    Votes = 12,
                    Answers = 1,
                    Title = "Question title -5",
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Question
                {
                    Id = -6,
                    Votes = 0,
                    Answers = 1,
                    Title = "Question title -6",
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                }
            });

            builder.Entity<Answer>().HasData(new Answer[]
            {
                new Answer
                {
                    Id = -1,
                    QuestionId = -1,
                    Votes = 12,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Answer
                {
                    Id = -2,
                    QuestionId = -1,
                    Votes = 10,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Answer
                {
                    Id = -3,
                    QuestionId = -2,
                    Votes = 10,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Answer
                {
                    Id = -4,
                    QuestionId = -3,
                    Votes = 10,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Answer
                {
                    Id = -5,
                    QuestionId = -4,
                    Votes = 10,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Answer
                {
                    Id = -6,
                    QuestionId = -5,
                    Votes = 10,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                }
            });

            builder.Entity<Comment>().HasData(new Comment[]
            {
                new Comment
                {
                    Id = -1,
                    QuestionId = -1,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Comment
                {
                    Id = -2,
                    QuestionId = -1,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Comment
                {
                    Id = -3,
                    QuestionId = -1,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Comment
                {
                    Id = -4,
                    QuestionId = -2,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Comment
                {
                    Id = -5,
                    QuestionId = -3,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Comment
                {
                    Id = -6,
                    QuestionId = -4,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Comment
                {
                    Id = -7,
                    QuestionId = -5,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Comment
                {
                    Id = -8,
                    AnswerId = -1,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Comment
                {
                    Id = -9,
                    AnswerId = -2,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Comment
                {
                    Id = -10,
                    AnswerId = -3,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Comment
                {
                    Id = -11,
                    AnswerId = -4,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                new Comment
                {
                    Id = -12,
                    AnswerId = -4,
                    Description = "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}",
                    CreateDateTime = new DateTime(2021, 5, 5),
                    UserId = userId2
                },
                
            });
        }
    }
}