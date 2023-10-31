using DeviceManagementDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManagementDB.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DeviceManagementContext _context;
        private DbSet<TEntity> DbEntity;
        public BaseRepository(DeviceManagementContext context)
        {
            _context = context;
            DbEntity = _context.Set<TEntity>();
        }
        public int Delete(int id)
        {
            TEntity model = DbEntity.Find(id);
            DbEntity.Remove(model);

            return _context.SaveChanges();
        }

        public List<TEntity> GetAll()
        {
            return DbEntity.ToList();
        }

        public TEntity GetById(int id)
        {
            return DbEntity.Find(id);
        }

        public int Insert(TEntity request)
        {
            DbEntity.Add(request);
            return _context.SaveChanges();
        }

        public int Update(TEntity request)
        {
            DbEntity.Update(request);
            return _context.SaveChanges();
        }
    }
}
