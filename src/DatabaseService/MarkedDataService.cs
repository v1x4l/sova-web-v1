using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomainModel;

namespace DatabaseService
{
    public class MarkedDataService : IDataService<Marked>
    {
        SovaContext db;
        public MarkedDataService(SovaContext db){
            this.db = db;
        }

        public void Add(Marked someDbObject)
        {
                someDbObject.MarkedId = db.Markeds.Max(m => m.MarkedId) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
        }

        public int Count()
        {
                return db.Markeds.Count();
        }

        public bool Delete(int id)
        {
                var Marked = db.Markeds.FirstOrDefault(m => m.MarkedId == id);
                if (Marked == null)
                {
                    return false;
                }
                db.Remove(Marked);
                return db.SaveChanges() > 0;
        }

        public Marked Get(int id)
        {
                return db.Markeds.FirstOrDefault(m => m.MarkedId == id);
        }

        public IList<Marked> GetList(int page, int pageSize)
        {
                return
                    db.Markeds
                    .OrderBy(m => m.MarkedId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public IList<Marked> GetProcedureList(int page, int pageSize, string word1, string word2, string word3, bool questionSearch)
        {
            throw new NotImplementedException();
        }

        public bool Update(Marked someDbObject)
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