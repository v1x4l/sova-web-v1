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
    
        SovaContext db;

        public SovaUserDataService(SovaContext db){
        this.db = db;
    }

    public void Add(SovaUser someDbObject)
    {
            try
            {
                someDbObject.SovaUserId = db.SovaUsers.Max(su => su.SovaUserId) + 1;
            }
            catch {
                someDbObject.SovaUserId = 1;
             }
            
            db.Add(someDbObject);
            db.SaveChanges();
    }

    public int Count()
    {
        return db.SovaUsers.Count();
    }

    public bool Delete(int id)
    {
        var sovaUser = db.SovaUsers.FirstOrDefault(su => su.SovaUserId == id);
        if (sovaUser == null)
        {
            return false;
        }
        db.Remove(sovaUser);
        return db.SaveChanges() > 0;
    }

    public SovaUser Get(int id)
    {
        return db.SovaUsers.FirstOrDefault(su => su.SovaUserId == id);
    }

    public IList<SovaUser> GetList(int page, int pageSize)
    {
        return
            db.SovaUsers
            .OrderBy(su => su.SovaUserId)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
    }

        public IList<SovaUser> GetProcedureList(int page, int pageSize, string word1, string word2, string word3)
        {
            throw new NotImplementedException();
        }

        public bool Update(SovaUser someDbObject)
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

