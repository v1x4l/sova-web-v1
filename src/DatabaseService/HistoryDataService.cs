using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace DatabaseService
{
    public class HistoryDataService : IDataService<History>
    {
        SovaContext db;
        public HistoryDataService(SovaContext db) {
            this.db = db;
        }

        public void Add(History someDbObject)
        {
                someDbObject.HistoryId = db.Histories.Max(h => h.HistoryId) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
        }

        public int Count()
        {
                return db.Histories.Count();
        }

        public bool Delete(int id)
        {
                var History = db.Histories.FirstOrDefault(h => h.HistoryId == id);
                if (History == null)
                {
                    return false;
                }
                db.Remove(History);
                return db.SaveChanges() > 0;
        }

        public History Get(int id)
        {
                return db.Histories.FirstOrDefault(h => h.HistoryId == id);
        }

        public IList<History> GetList(int page, int pageSize)
        {
                return
                    db.Histories
                    .OrderBy(m => m.HistoryId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public bool Update(History someDbObject)
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
