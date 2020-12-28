using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebHpShop.Reponsitory.Interfaces;
using WebHpShop.Service.Interfaces;

namespace WebHpShop.Service.Services
{
    public class BaseService<T> : IServices<T> where T : class
    {
        private readonly IReponsitory<T> _baseReponsitory;

        public BaseService(IReponsitory<T> baseReponsitory)
        {
            _baseReponsitory = baseReponsitory;
        }

        #region Async

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _baseReponsitory.FindAllAsync(match);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _baseReponsitory.FindAsync(match);
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _baseReponsitory.GetByIdAsync(id);
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await _baseReponsitory.GetAllAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            return await _baseReponsitory.AddAsync(entity);
        }

        public async Task<bool> AddManyAsync(IEnumerable<T> entities)
        {
            return await _baseReponsitory.AddManyAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            await _baseReponsitory.UpdateAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await _baseReponsitory.DeleteAsync(entity);
        }

        #endregion

        #region Normal

        public T GetById(long id)
        {
            return _baseReponsitory.GetById(id);
        }

        public IQueryable<T> GetAll()
        {
            return _baseReponsitory.GetAll();
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return _baseReponsitory.Find(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _baseReponsitory.FindAll(match);
        }

        public T Get(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public bool AddMany(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
