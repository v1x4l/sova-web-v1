using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomainModel;

namespace DatabaseService
{
    public class TopicDataService : IDataService<Topic>
    {
        SovaContext db;
        public TopicDataService(SovaContext db) {
            this.db = db;
        }

        public void Add(Topic someDbObject)
        {
                someDbObject.TopicId = db.Topics.Max(t => t.TopicId) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
        }

        public int Count()
        {
                return db.Topics.Count();
        }

        public bool Delete(int id)
        {
                var topic = db.Topics.FirstOrDefault(t => t.TopicId == id);
                if (topic == null)
                {
                    return false;
                }
                db.Remove(topic);
                return db.SaveChanges() > 0;
        }

        public Topic Get(int id)
        {
                return db.Topics.FirstOrDefault(t => t.TopicId == id);
        }

        public IList<Topic> GetList(int page, int pageSize)
        {
                return
                    db.Topics
                    .OrderBy(t => t.TopicId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public bool Update(Topic someDbObject)
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