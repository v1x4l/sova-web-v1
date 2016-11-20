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
        public void Add(Topic someDbObject)
        {
            using (var db = new SovaContext())
            {
                someDbObject.TopicId = db.Topics.Max(t => t.TopicId) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
            }
        }

        public int Count()
        {
            using (var db = new SovaContext())
            {
                return db.Topics.Count();
            }
        }

        public bool Delete(int id)
        {
            using (var db = new SovaContext())
            {
                var topic = db.Topics.FirstOrDefault(t => t.TopicId == id);
                if (topic == null)
                {
                    return false;
                }
                db.Remove(topic);
                return db.SaveChanges() > 0;
            }
        }

        public Topic Get(int id)
        {
            using (var db = new SovaContext())
            {
                return db.Topics.FirstOrDefault(t => t.TopicId == id);
            }
        }

        public IList<Topic> GetList(int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                return
                    db.Topics
                    .OrderBy(t => t.TopicId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public bool Update(Topic someDbObject)
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