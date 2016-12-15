
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace DatabaseService
{
    public class SovaContext : DbContext
    {

        public SovaContext() {

        }

        public SovaContext(DbContextOptions<SovaContext> options)
        : base(options)
        { }

        public DbSet<SearchResult> SearchResults { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<SovaUser> SovaUsers { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<PostTopic> PostTopics { get; set; }
        public DbSet<Marked> Markeds { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FrequentWord> FrequentWord { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //the frequent words procedure
            modelBuilder.Entity<FrequentWord>().HasKey(t => new { t.Word, t.Frequency});

            //the search procedure
            modelBuilder.Entity<SearchResult>().HasKey(t => new { t.PostId, t.PostTitle, t.PostText, t.PostScore, t.Rank });

            //history table
            modelBuilder.Entity<History>().ToTable("history");
            modelBuilder.Entity<History>().Property(h => h.SovaUserId).HasColumnName("sovauserid");
            modelBuilder.Entity<History>().Property(h => h.HistoryId).HasColumnName("historyid");
            modelBuilder.Entity<History>().Property(h => h.SearchText).HasColumnName("searchtext");
            modelBuilder.Entity<History>().Property(h => h.SearchDate).HasColumnName("searchdate");

            //the comment table
            modelBuilder.Entity<Comment>().ToTable("comment");
            modelBuilder.Entity<Comment>().Property(c => c.CommentId).HasColumnName("commentid");
            modelBuilder.Entity<Comment>().Property(c => c.PostId).HasColumnName("postid");
            modelBuilder.Entity<Comment>().Property(c => c.UserId).HasColumnName("userid");
            modelBuilder.Entity<Comment>().Property(c => c.CommentText).HasColumnName("commenttext");
            modelBuilder.Entity<Comment>().Property(c => c.CommentCreationDate).HasColumnName("commentcreatedate");
            modelBuilder.Entity<Comment>().Property(c => c.CommentScore).HasColumnName("commentscore");

            //marked table
            modelBuilder.Entity<Marked>().ToTable("marked");
            modelBuilder.Entity<Marked>().Property(m => m.MarkedId).HasColumnName("markedid");
            modelBuilder.Entity<Marked>().Property(m => m.PostId).HasColumnName("postid");
            modelBuilder.Entity<Marked>().Property(m => m.Note).HasColumnName("note");
            modelBuilder.Entity<Marked>().Property(m => m.Date).HasColumnName("date");


            //sovaUser table
            modelBuilder.Entity<SovaUser>().ToTable("sovauser");
            modelBuilder.Entity<SovaUser>().Property(su => su.SovaUserId).HasColumnName("sovauserid");
            modelBuilder.Entity<SovaUser>().Property(su => su.SovaUserCreationDate).HasColumnName("sovausercreationdate");


            //The post table
            modelBuilder.Entity<Topic>().ToTable("topic");
            modelBuilder.Entity<Topic>().Property(t => t.TopicId).HasColumnName("topicid");
            modelBuilder.Entity<Topic>().Property(t => t.Frequency).HasColumnName("frequency");
            modelBuilder.Entity<Topic>().Property(t => t.TopicName).HasColumnName("topicname");


            //The PostTopic table
            modelBuilder.Entity<PostTopic>().ToTable("posttopic");
            modelBuilder.Entity<PostTopic>().Property(pt => pt.IdPostTopic).HasColumnName("idposttopic");
            modelBuilder.Entity<PostTopic>()
                .HasKey(pt => pt.IdPostTopic);
            modelBuilder.Entity<PostTopic>().Property(pt => pt.PostId).HasColumnName("postid");
            modelBuilder.Entity<PostTopic>().Property(pt => pt.TopicId).HasColumnName("topicid");



            //Table answer mangler datetime
            modelBuilder.Entity<Answer>().ToTable("answer");
            modelBuilder.Entity<Answer>().Property(a => a.AnswerId).HasColumnName("answerid");
            modelBuilder.Entity<Answer>().Property(a => a.UserId).HasColumnName("userid");
            modelBuilder.Entity<Answer>().Property(a => a.ParentId).HasColumnName("parentid");
            modelBuilder.Entity<Answer>().Property(a => a.AnswerCreationDate).HasColumnName("answercreationdate");
            modelBuilder.Entity<Answer>().Property(a => a.AnswerScore).HasColumnName("answerscore");
            modelBuilder.Entity<Answer>().Property(a => a.AnswerText).HasColumnName("answertext");


            //The user table
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<User>().Property(u => u.UserId).HasColumnName("userid");
            modelBuilder.Entity<User>().Property(u => u.UserDisplayName).HasColumnName("userdisplayname");
            modelBuilder.Entity<User>().Property(u => u.UserCreationDate).HasColumnName("usercreationdate");
            modelBuilder.Entity<User>().Property(u => u.UserLocation).HasColumnName("userlocation");
            modelBuilder.Entity<User>().Property(u => u.UserAge).HasColumnName("userage");

            //The question table
            modelBuilder.Entity<Question>().ToTable("question");
            modelBuilder.Entity<Question>().Property(q => q.QuestionId).HasColumnName("questionid");
            modelBuilder.Entity<Question>().Property(q => q.UserId).HasColumnName("userid");
            modelBuilder.Entity<Question>().Property(q => q.AcceptedAnswerId).HasColumnName("acceptedanswerid");
            modelBuilder.Entity<Question>().Property(q => q.QuestionCreationDate).HasColumnName("questioncreatingdate");
            modelBuilder.Entity<Question>().Property(q => q.QuestionText).HasColumnName("questiontext");
            modelBuilder.Entity<Question>().Property(q => q.QuestionScore).HasColumnName("questionscore");
            modelBuilder.Entity<Question>().Property(q => q.QuestionTitle).HasColumnName("questiontitle");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=sova;uid=root;pwd=password");
            }
        }
    }
}
