using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace DatabaseService
{
    public class AnswerDataService : IDataService<Answer>
    {
        SovaContext db;
        public AnswerDataService(SovaContext db) {
            this.db = db;
        }

        public void Add(Answer someDbObject)
        {
                someDbObject.AnswerId = db.Answers.Max(a => a.AnswerId) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
        }

        public int Count()
        {
                return db.Answers.Count();
        }

        public bool Delete(int id)
        {
                var answer = db.Answers.FirstOrDefault(a => a.AnswerId == id);
                if (answer == null)
                {
                    return false;
                }
                db.Remove(answer);
                return db.SaveChanges() > 0;
        }

        public Answer Get(int id)
        {
                return db.Answers.FirstOrDefault(a => a.AnswerId == id);
        }

        public IList<Answer> GetList(int page, int pageSize)
        {
                return
                    db.Answers
                    .OrderBy(a => a.AnswerId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public bool Update(Answer someDbObject)
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