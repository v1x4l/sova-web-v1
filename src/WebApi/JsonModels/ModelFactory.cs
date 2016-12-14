using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.JsonModels
{
    public class ModelFactory
    {
        public static SovaUserModel Map(SovaUser sovaUser, IUrlHelper url)
        {
            return new SovaUserModel
            {
                Url = url.Link(Config.SovaUserRoute, new { id = sovaUser.SovaUserId }),
                SovaUserId = sovaUser.SovaUserId,
                SovaUserCreationDate = sovaUser.SovaUserCreationDate
            };

        }

        public static SovaUser Map(SovaUserModel model)
        {
            return new SovaUser
            {
                SovaUserCreationDate = model.SovaUserCreationDate

            };
        }

        public static SearchResultModel Map(SearchResult searchResult, IUrlHelper url)
        {
            return new SearchResultModel
            {
                Url = url.Link(Config.SearchResultRoute, new { id = searchResult.PostId }),
                PostId = searchResult.PostId,
                PostText = searchResult.PostText,
                Rank = searchResult.Rank
                
            };

        }

        public static SearchResult Map(SearchResultModel model)
        {
            return new SearchResult
            {
                PostId = model.PostId,
                PostText = model.PostText,
                Rank = model.Rank
            };
        }

        public static UserModel Map(User user, IUrlHelper url)
        {
            return new UserModel
            {
                Url = url.Link(Config.UserRoute, new { id = user.UserId }),
                UserId = user.UserId,
                UserDisplayName = user.UserDisplayName,
                UserCreationDate = user.UserCreationDate,
                UserLocation = user.UserLocation,
                UserAge = user.UserAge,
            };
        }

        public static User Map(UserModel model)
        {
            return new User
            {
                UserDisplayName = model.UserDisplayName,
                UserCreationDate = model.UserCreationDate,
                UserLocation = model.UserLocation,
                UserAge = model.UserAge,
            };
        }

        public static TopicModel Map(Topic topic, IUrlHelper url)
        {
            return new TopicModel
            {
                Url = url.Link(Config.TopicRoute, new { id = topic.TopicId }),
                TopicId = topic.TopicId,
                Frequency = topic.Frequency,
                TopicName = topic.TopicName
            };
        }

       
        public static Topic Map(TopicModel model)
        {
            return new Topic
            {
                Frequency = model.Frequency,
                TopicName = model.TopicName
            };
        }

        public static QuestionModel Map(Question question, IUrlHelper url)
        {
            return new QuestionModel
            {
                Url = url.Link(Config.QuestionRoute, new { id = question.QuestionId }),
                QuestionId = question.QuestionId,
                UserId = question.UserId,
                AcceptedAnswerId = question.AcceptedAnswerId,
                QuestionCreationDate = question.QuestionCreationDate,
                QuestionText = question.QuestionText,
                QuestionScore = question.QuestionScore,
                QuestionTitle = question.QuestionTitle
            };
        }

        public static Question Map(QuestionModel model)
        {
            return new Question
            {
                UserId = model.UserId,
                AcceptedAnswerId = model.AcceptedAnswerId,
                QuestionCreationDate = model.QuestionCreationDate,
                QuestionText = model.QuestionText,
                QuestionScore = model.QuestionScore,
                QuestionTitle = model.QuestionTitle
            };
        }

        public static PostTopicModel Map(PostTopic postTopic, IUrlHelper url)
        {
            return new PostTopicModel
            {
                Url = url.Link(Config.PostTopicRoute, new { id = postTopic.IdPostTopic }),
                IdPostTopic = postTopic.IdPostTopic,
                PostId = postTopic.PostId,
                TopicId = postTopic.TopicId
            };
        }

        public static PostTopic Map(PostTopicModel model)
        {
            return new PostTopic
            {
                PostId = model.PostId,
                TopicId = model.TopicId
            };
        }

        public static MarkedModel Map(Marked marked, IUrlHelper url)
        {
            return new MarkedModel
            {
                Url = url.Link(Config.MarkedRoute, new { id = marked.MarkedId }),
                MarkedId = marked.PostId,
                PostId = marked.PostId,
                Note = marked.Note,
                Date = marked.Date

            };
        }

        public static Marked Map(MarkedModel model)
        {
            return new Marked
            {
                PostId = model.PostId,
                Note = model.Note,
                Date = model.Date
            };
        }

        public static HistoryModel Map(History history, IUrlHelper url)
        {
            return new HistoryModel
            {
                Url = url.Link(Config.HistoryRoute, new { id = history.HistoryId }),
                HistoryId = history.HistoryId,
                SovaUserId = history.SovaUserId,
                SearchText = history.SearchText,
                SearchDate = history.SearchDate
            };
        }

        public static History Map(HistoryModel model)
        {
            return new History
            {
                SovaUserId = model.SovaUserId,
                SearchText = model.SearchText,
                SearchDate = model.SearchDate
            };
        }

        public static CommentModel Map(Comment comment, IUrlHelper url)
        {
            return new CommentModel
            {
                Url = url.Link(Config.CommentRoute, new { id = comment.CommentId }),
                CommentId = comment.CommentId,
                PostId = comment.PostId,
                UserId = comment.UserId,
                CommentText = comment.CommentText,
                CommentCreationDate = comment.CommentCreationDate,
                CommentScore = comment.CommentScore

            };
        }

        public static Comment Map(CommentModel model)
        {
            return new Comment
            {
                PostId = model.PostId,
                UserId = model.UserId,
                CommentText = model.CommentText,
                CommentCreationDate = model.CommentCreationDate,
                CommentScore = model.CommentScore
            };
        }

        public static AnswerModel Map(Answer answer, IUrlHelper url)
        {
            return new AnswerModel
            {
                Url = url.Link(Config.AnswersRoute, new { id = answer.AnswerId }),
                AnswerId = answer.AnswerId,
                UserId = answer.UserId,
                ParentId = answer.ParentId,
                AnswerCreationDate = answer.AnswerCreationDate,
                AnswerScore = answer.AnswerScore,
                AnswerText = answer.AnswerText

            };
        }

        public static Answer Map(AnswerModel model)
        {
            return new Answer
            {
                UserId = model.UserId,
                ParentId = model.ParentId,
                AnswerCreationDate = model.AnswerCreationDate,
                AnswerScore = model.AnswerScore,
                AnswerText = model.AnswerText
            };
        }
    }
}
