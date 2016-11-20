using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.JsonModels
{
    public class CommentModel
    {
        public string Url { get; set; }
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentCreationDate { get; set; }
        public int CommentScore { get; set; }

    }

    public class MarkedModel
    {
        public string Url { get; set; }
        public int MarkedId { get; set; }
        public int PostId { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }

    public class SovaUserModel
    {
        public string Url { get; set; }
        public int SovaUserId { get; set; }
        public DateTime SovaUserCreationDate { get; set; }
    }
    public class PostTopicModel
    {
        public string Url { get; set; }
        public int IdPostTopic { get; set; }
        public int PostId { get; set; }
        public int TopicId { get; set; }
    }


    public class TopicModel
    {
        public string Url { get; set; }
        public int TopicId { get; set; }
        public int Frequency { get; set; }
        public string TopicName { get; set; }

    }

    public class AnswerModel
    {
        public string Url { get; set; }
        public int AnswerId { get; set; }
        public int UserId { get; set; }
        public int ParentId { get; set; }
        public DateTime AnswerCreationDate { get; set; }
        public int AnswerScore { get; set; }
        public string AnswerText { get; set; }
    }

    public class UserModel
    {
        public string Url { get; set; }
        public int UserId { get; set; }
        public string UserDisplayName { get; set; }
        public DateTime UserCreationDate { get; set; }
        public string UserLocation { get; set; }
        public int? UserAge { get; set; }


    }

    public class QuestionModel
    {
        public string Url { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public DateTime QuestionCreationDate { get; set; }
        public string QuestionText { get; set; }
        public int QuestionScore { get; set; }
        public string QuestionTitle { get; set; }
    }

    public class HistoryModel
    {
        public string Url { get; set; }
        public int HistoryId { get; set; }
        public int SovaUserId { get; set; }
        public string SearchText { get; set; }
        public DateTime SearchDate { get; set; }
    }


}
