using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.EntityFrameworkCore;


namespace DatabaseService
{
    public class CommentDataService : IDataService<Comment>
    {
        public void Add(Comment someDbObject)
        {
            using (var db = new SovaContext())
            {
                someDbObject.CommentId = db.Comments.Max(c => c.CommentId) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
            }
        }

        public int Count()
        {
            using (var db = new SovaContext())
            {
                return db.Comments.Count();
            }
        }

        public bool Delete(int id)
        {
            using (var db = new SovaContext())
            {
                var Comment = db.Comments.FirstOrDefault(c => c.CommentId == id);
                if (Comment == null)
                {
                    return false;
                }
                db.Remove(Comment);
                return db.SaveChanges() > 0;
            }
        }

        public Comment Get(int id)
        {
            using (var db = new SovaContext())
            {
                return db.Comments.FirstOrDefault(c => c.CommentId == id);
            }
        }

        public IList<Comment> GetList(int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                return
                    db.Comments
                    .OrderBy(m => m.CommentId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public bool Update(Comment someDbObject)
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