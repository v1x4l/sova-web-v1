using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace DatabaseService
{
    public class UserDataService : IDataService<User>
    {
        
        public IList<User> GetList(int page, int pagesize)
        {
            using (var db = new SovaContext())
            {
                return db.Users
                    .OrderBy(u => u.UserId)
                    .Skip(page * pagesize)
                    .Take(pagesize)
                    .ToList();
            }
        }

        public User Get(int id)
        {
            using (var db = new SovaContext())
            {
                return db.Users.FirstOrDefault(c => c.UserId == id);
            }
        }

        public void Add(User user)
        {
            using (var db = new SovaContext())
            {
                user.UserId = db.Users.Max(u => u.UserId) + 1;
                db.Add(user);
                db.SaveChanges();
            }
        }

        public bool Update(User user)
        {
            using (var db = new SovaContext())

                try
                {
                    db.Attach(user);
                    db.Entry(user).State = EntityState.Modified;
                    return db.SaveChanges() > 0;
                }
                catch (DbUpdateConcurrencyException)
                {
                    return false;
                }

        }

        public bool Delete(int id)
        {
            using (var db = new SovaContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserId == id);
                if (user == null)
                {
                    return false;
                }
                db.Remove(user);
                return db.SaveChanges() > 0;
            }
        }

        public int Count()
        {
            using (var db = new SovaContext())
            {
                return db.Users.Count();
            }
        }
    }
}
