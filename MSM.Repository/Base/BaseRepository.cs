using MSM.IRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MSM.Model;
using MSM.Model.Entity;
using Z.EntityFramework.Plus;
using Microsoft.EntityFrameworkCore;

namespace MSM.Repository.Base
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, new()
        where TKey : struct
    {

        protected readonly MsmDbContext msmDbContext;

        public BaseRepository(MsmDbContext _msmDbContext)
        {
            this.msmDbContext = _msmDbContext;
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public virtual async Task CreateAsync(TEntity Entity)
        {
            await msmDbContext.AddAsync<TEntity>(Entity);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public virtual void Update(TEntity Entity)
        {
            msmDbContext.Set<TEntity>().Update(Entity);
        }

        /// <summary>
        /// 根据条件进行更新(批量)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TEntity>> entity)
        {
            return await msmDbContext.Set<TEntity>().Where(whereLambda).UpdateAsync(entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public virtual void Deleted(TEntity Entity)
        {
            msmDbContext.Entry<TEntity>(Entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// 根据条件进行删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> whereLambda)
        {
            return await msmDbContext.Set<TEntity>().Where(whereLambda).DeleteAsync();
        }

        /// <summary>
        /// 判断指定实体是否存在
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsExist(Expression<Func<TEntity, bool>> whereLambda)
        {
            return await msmDbContext.Set<TEntity>().AnyAsync(whereLambda);
        }

        /// <summary>
        /// 根据条件表达式获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await msmDbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// 根据条件表达式获取集合-延迟查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> FindQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return msmDbContext.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// 根据主键值返回单条实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(TKey ID)
        {
            return await msmDbContext.Set<TEntity>().FindAsync(ID);
        }

        /// <summary>
        /// 根据条件表达式返回第一个对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await msmDbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 异步获取所有数据
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await msmDbContext.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public virtual List<TEntity> GetAll()
        {
            return msmDbContext.Set<TEntity>().ToList();
        }
    }
}
