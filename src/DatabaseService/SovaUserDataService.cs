using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomainModel;

namespace DatabaseService
{
    public class SovaUserDataService : IDataService<SovaUser>
    {
        public void Add(SovaUser someDbObject)
        {
            using (var db = new SovaContext())
            {
                someDbObject.SovaUserId = db.SovaUsers.Max(su => su.SovaUserId) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
            }
        }

        public int Count()
        {
            using (var db = new SovaContext())
            {
                return db.SovaUsers.Count();
            }
        }

        public bool Delete(int id)
        {
            using (var db = new SovaContext())
            {
                var sovaUser = db.SovaUsers.FirstOrDefault(su => su.SovaUserId == id);
                if (sovaUser == null)
                {
                    return false;
                }
                db.Remove(sovaUser);
                return db.SaveChanges() > 0;
            }
        }

        public SovaUser Get(int id)
        {
            using (var db = new SovaContext())
            {
                return db.SovaUsers.FirstOrDefault(su => su.SovaUserId == id);
            }
        }

        public IList<SovaUser> GetList(int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                return
                    db.SovaUsers
                    .OrderBy(su => su.SovaUserId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public bool Update(SovaUser someDbObject)
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