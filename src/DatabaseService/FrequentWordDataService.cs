using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace DatabaseService
{
    public class FrequentWordDataService : IDataService<FrequentWord>
    {
        public void Add(FrequentWord someDbObject)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public FrequentWord Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<FrequentWord> GetList(int page, int pagesize)
        {
            throw new NotImplementedException();
        }

        public IList<FrequentWord> GetProcedureList(int page, int pageSize, string word1, string word2, string word3, bool questionSearch)
        {
            List<FrequentWord> results = new List<FrequentWord>();

            using (SovaContext db = new SovaContext())
            {
                if (questionSearch) {
                    var something = db.Set<FrequentWord>()
                    .FromSql("call relevantQuestionWords({0},{1},{2})", new[] { word1, word2, word3 })
                    .Skip(page * pageSize)
                    .Take(pageSize);

                    foreach (var data in something)
                    {
                        FrequentWord tmp = new FrequentWord { };
                        tmp.Frequency = data.Frequency;
                        tmp.Word = data.Word;
                        results.Add(tmp);
                    }
                }
                else {
                    var something = db.Set<FrequentWord>()
                    .FromSql("call relevantAnswerWords({0},{1},{2})", new[] { word1, word2, word3 })
                    .Skip(page * pageSize)
                    .Take(pageSize);

                    foreach (var data in something)
                    {
                        FrequentWord tmp = new FrequentWord { };
                        tmp.Frequency = data.Frequency;
                        tmp.Word = data.Word;
                        results.Add(tmp);
                    }
                }
                

                return results;
            }
        }

        public bool Update(FrequentWord someDbObject)
        {
            throw new NotImplementedException();
        }
    }
}
