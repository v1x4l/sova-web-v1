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
        SovaContext db;
        public CommentDataService(SovaContext db) {
            this.db = db;
        }
        public void Add(Comment someDbObject)
        {
                someDbObject.CommentId = db.Comments.Max(c => c.CommentId) + 1;
                db.Add(someDbObject);
                db.SaveChanges();
        }

        public int Count()
        {
                return db.Comments.Count();
        }

        public bool Delete(int id)
        {
                var Comment = db.Comments.FirstOrDefault(c => c.CommentId == id);
                if (Comment == null)
                {
                    return false;
                }
                db.Remove(Comment);
                return db.SaveChanges() > 0;
        }

        public Comment Get(int id)
        {
                return db.Comments.FirstOrDefault(c => c.CommentId == id);
        }

        public IList<Comment> GetList(int page, int pageSize)
        {
                return
                    db.Comments
                    .OrderBy(m => m.CommentId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public IList<Comment> GetProcedureList(int page, int pageSize, string word1, string word2, string word3, bool questionSearch)
        {
            throw new NotImplementedException();
        }

        public bool Update(Comment someDbObject)
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