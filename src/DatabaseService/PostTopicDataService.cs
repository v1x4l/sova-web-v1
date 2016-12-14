using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomainModel;

namespace DatabaseService
{
    public class PostTopicDataService : IDataService<PostTopic>
    {
        SovaContext db;
        public PostTopicDataService(SovaContext db) {
            this.db = db;
        }

        public void Add(PostTopic someDbObject)
        {
                someDbObject.IdPostTopic = db.PostTopics.Max(pt => pt.IdPostTopic) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
        }

        public int Count()
        {
                return db.PostTopics.Count();
        }

        public bool Delete(int id)
        {
                var postTopic = db.PostTopics.FirstOrDefault(pt => pt.IdPostTopic == id);
                if (postTopic == null)
                {
                    return false;
                }
                db.Remove(postTopic);
                return db.SaveChanges() > 0;
        }

        public PostTopic Get(int id)
        {
                return db.PostTopics.FirstOrDefault(pt => pt.IdPostTopic == id);
        }

        public IList<PostTopic> GetList(int page, int pageSize)
        {
                return
                    db.PostTopics
                    .OrderBy(pt => pt.IdPostTopic)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public IList<PostTopic> GetProcedureList(int page, int pageSize, string word1, string word2, string word3)
        {
            throw new NotImplementedException();
        }

        public bool Update(PostTopic someDbObject)
        {
         
                try
                {
                    db.Attach(someDbObject);
                    db.Entry(someDbObject).State = EntityState.Modified;
                    return db.SaveChanges() > 0;
                }
                catch (DbUpdateConcurrencyException)
                {
                    return false;
                }
        }
    }
}