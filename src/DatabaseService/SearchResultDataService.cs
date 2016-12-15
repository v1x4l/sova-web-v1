using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomainModel;

namespace DatabaseService
{
    public class SearchResultDataService : IDataService<SearchResult>
    {
        SovaContext db;

        public SearchResultDataService(SovaContext db) {
            this.db = db;
        }

        public void Add(SearchResult someDbObject)
        {
            throw new NotImplementedException();
        }

        public IList<SearchResult> GetProcedureList(int page, int pageSize, string word1, string word2, string word3, bool questionSearch)
        {
            List<SearchResult> results = new List<SearchResult>();
            using (SovaContext db = new SovaContext())
            {
                if (questionSearch) {
                    var resultOfQuestionSearch = db.Set<SearchResult>()
                    .FromSql("call questionSearch({0},{1},{2})", new[] { word1, word2, word3 })
                    .Skip(page * pageSize)
                    .Take(pageSize);
                    foreach (var data in resultOfQuestionSearch)
                    {
                        SearchResult tmp = new SearchResult { };
                        tmp.PostId = data.PostId;
                        tmp.PostTitle = data.PostTitle;
                        tmp.PostScore = data.PostScore;
                        tmp.PostText = data.PostText;
                        tmp.Rank = data.Rank;
                        results.Add(tmp);
                    }

                }
                else {
                    var resultOfAnswerSearch = db.Set<SearchResult>()
                    .FromSql("call answerSearch({0},{1},{2})", new[] { word1, word2, word3 })
                    .Skip(page * pageSize)
                    .Take(pageSize);

                    foreach (var data in resultOfAnswerSearch)
                    {
                        SearchResult tmp = new SearchResult { };
                        tmp.PostId = data.PostId;
                        tmp.PostTitle = data.PostTitle;
                        tmp.PostScore = data.PostScore;
                        tmp.PostText = data.PostText;
                        tmp.Rank = data.Rank;
                        results.Add(tmp);
                    }

                }
                return results;
            }
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public SearchResult Get(int id)
        {
            return db.SearchResults.FirstOrDefault(sr => sr.PostId == id);
        }

        public IList<SearchResult> GetList(int page, int pagesize)
        {
            throw new NotImplementedException();
        }



        public bool Update(SearchResult someDbObject)
        {
            throw new NotImplementedException();
        }
    }
}