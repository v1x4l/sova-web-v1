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
        public void Add(PostTopic someDbObject)
        {
            using (var db = new SovaContext())
            {
                someDbObject.IdPostTopic = db.PostTopics.Max(pt => pt.IdPostTopic) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
            }
        }

        public int Count()
        {
            using (var db = new SovaContext())
            {
                return db.PostTopics.Count();
            }
        }

        public bool Delete(int id)
        {
            using (var db = new SovaContext())
            {
                var postTopic = db.PostTopics.FirstOrDefault(pt => pt.IdPostTopic == id);
                if (postTopic == null)
                {
                    return false;
                }
                db.Remove(postTopic);
                return db.SaveChanges() > 0;
            }
        }

        public PostTopic Get(int id)
        {
            using (var db = new SovaContext())
            {
                return db.PostTopics.FirstOrDefault(pt => pt.IdPostTopic == id);
            }
        }

        public IList<PostTopic> GetList(int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                return
                    db.PostTopics
                    .OrderBy(pt => pt.IdPostTopic)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public bool Update(PostTopic someDbObject)
        {
            using (var db = new SovaContext())

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