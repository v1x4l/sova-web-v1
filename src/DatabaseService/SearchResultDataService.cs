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

        private int limitBy = 10; //total amount of searchResults

        public SearchResultDataService(SovaContext db) {
            this.db = db;
        }

        public void Add(SearchResult someDbObject)
        {
            throw new NotImplementedException();
        }

        public IList<SearchResult> GetProcedureList(int page, int pageSize, string word1, string word2, string word3)
        {

            List<SearchResult> results = new List<SearchResult>();
            using (SovaContext db = new SovaContext())
            {
                var something = db.Set<SearchResult>()
                    .FromSql("call advancedSearch({0},{1},{2})", new[] { word1, word2, word3 })
                    .Skip(page * pageSize)
                    .Take(pageSize);
                foreach (var data in something)
                {
                    SearchResult tmp = new SearchResult { };
                    tmp.PostId = data.PostId;
                    tmp.PostText = data.PostText;
                    tmp.Rank = data.Rank;
                    results.Add(tmp);
                }

                return results;
            }
        }

        public int Count()
        {
            throw new NotImplementedException();
            /*
            if (db.SearchResults != null)
            {
                return db.SearchResults.Count();
            }
            else {
                return 0;
            }
            */
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