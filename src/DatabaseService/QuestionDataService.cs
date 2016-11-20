using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomainModel;

namespace DatabaseService
{
    public class QuestionDataService : IDataService<Question>
    {
        public void Add(Question someDbObject)
        {
            using (var db = new SovaContext())
            {
                someDbObject.QuestionId = db.Questions.Max(q => q.QuestionId) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
            }
        }

        public int Count()
        {
            using (var db = new SovaContext())
            {
                return db.Questions.Count();
            }
        }

        public bool Delete(int id)
        {
            using (var db = new SovaContext())
            {
                var question = db.Questions.FirstOrDefault(q => q.QuestionId == id);
                if (question == null)
                {
                    return false;
                }
                db.Remove(question);
                return db.SaveChanges() > 0;
            }
        }

        public Question Get(int id)
        {
            using (var db = new SovaContext())
            {
                return db.Questions.FirstOrDefault(q => q.QuestionId == id);
            }
        }

        public IList<Question> GetList(int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                return
                    db.Questions
                    .OrderBy(m => m.QuestionId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public bool Update(Question someDbObject)
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