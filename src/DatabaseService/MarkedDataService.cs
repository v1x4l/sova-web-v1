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
        public void Add(Marked someDbObject)
        {
            using (var db = new SovaContext())
            {
                someDbObject.MarkedId = db.Markeds.Max(m => m.MarkedId) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
            }
        }

        public int Count()
        {
            using (var db = new SovaContext())
            {
                return db.Markeds.Count();
            }
        }

        public bool Delete(int id)
        {
            using (var db = new SovaContext())
            {
                var Marked = db.Markeds.FirstOrDefault(m => m.MarkedId == id);
                if (Marked == null)
                {
                    return false;
                }
                db.Remove(Marked);
                return db.SaveChanges() > 0;
            }
        }

        public Marked Get(int id)
        {
            using (var db = new SovaContext())
            {
                return db.Markeds.FirstOrDefault(m => m.MarkedId == id);
            }
        }

        public IList<Marked> GetList(int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                return
                    db.Markeds
                    .OrderBy(m => m.MarkedId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public bool Update(Marked someDbObject)
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