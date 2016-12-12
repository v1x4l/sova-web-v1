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
        SovaContext db;

        public QuestionDataService(SovaContext db) {
            this.db = db;
        }

        public void Add(Question someDbObject)
        {
                someDbObject.QuestionId = db.Questions.Max(q => q.QuestionId) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
        }

        public int Count()
        {
                return db.Questions.Count();
        }

        public bool Delete(int id)
        {
                var question = db.Questions.FirstOrDefault(q => q.QuestionId == id);
                if (question == null)
                {
                    return false;
                }
                db.Remove(question);
                return db.SaveChanges() > 0;
        }

        public Question Get(int id)
        {
                return db.Questions.FirstOrDefault(q => q.QuestionId == id);
        }

        public IList<Question> GetList(int page, int pageSize)
        {
                return
                    db.Questions
                    .OrderBy(m => m.QuestionId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public bool Update(Question someDbObject)
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