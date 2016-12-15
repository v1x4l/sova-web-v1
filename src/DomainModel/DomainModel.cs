using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel
{
    public class FrequentWord {
        public string Word { get; set; }
        public int Frequency { get; set; }
    }

    public class SearchResult {
        //The postId will be referring to either a question or answer as is
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostText { get; set; }
        public int PostScore { get; set; }
        public double Rank { get; set; }
    }

    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentCreationDate { get; set; }
        public int CommentScore { get; set; }

    }

    public class Marked
    {
        public int MarkedId { get; set; }
        public int PostId { get; set; }
        public int SovaUserId { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }

    public class SovaUser
    {
        public int SovaUserId { get; set; }
        public DateTime SovaUserCreationDate { get; set; }
    }
    public class PostTopic
    {
        public int IdPostTopic { get; set; }
        public int PostId { get; set; }
        public int TopicId { get; set; }
    }


    public class Topic
    {
        public int TopicId { get; set; }
        public int Frequency { get; set; }
        public string TopicName { get; set; }

    }

    public class Answer
    {
        public int AnswerId { get; set; }
        public int UserId { get; set; }
        public int ParentId { get; set; }
        public DateTime AnswerCreationDate { get; set; }
        public int AnswerScore { get; set; }
        public string AnswerText { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string UserDisplayName { get; set; }
        public DateTime UserCreationDate { get; set; }
        public string UserLocation { get; set; }
        public int? UserAge { get; set; }

    }

    public class Question
    {
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public DateTime QuestionCreationDate { get; set; }
        public string QuestionText { get; set; }
        public int QuestionScore { get; set; }
        public string QuestionTitle { get; set; }
    }

    public class History
    {
        public int HistoryId { get; set; }
        public int SovaUserId { get; set; }
        public string SearchText { get; set; }
        public DateTime SearchDate { get; set; }
    }

}
