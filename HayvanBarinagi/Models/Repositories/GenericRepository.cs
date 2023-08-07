using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HayvanBarinagi.Models.Repositories
{

    public class GenericRepositories<T> where T : class, new()
    {
        Context db = new Context();

        public List<T> List()
        {

            return db.Set<T>().ToList();

        }

        public virtual IQueryable<T> Queryable()
        {
            return db.Set<T>().AsQueryable();
        }

        public void Add(T p)
        {

            db.Set<T>().Add(p);
            db.SaveChanges();
        }

        public void Delete(T p)
        {

            db.Set<T>().Remove(p);
            db.SaveChanges();
        }

        public T TGet(int id)
        {
            return db.Set<T>().Find();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return db.Set<T>().FirstOrDefault(where);
        }

    }
}
